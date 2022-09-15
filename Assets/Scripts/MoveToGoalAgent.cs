using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private MeshRenderer Floor;
    [SerializeField] private Material Playing;
    [SerializeField] private Material Win;
    [SerializeField] private Material Lose;

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-3f, 0f), 1, Random.Range(-2f, 2f));
        targetTransform.localPosition = new Vector3(Random.Range(1f, 3f), 0.75f, Random.Range(-2f, 2f));
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn")
        {
            Floor.material = Lose;
            SetReward(-1f);
            EndEpisode();
        }
        else if(other.tag == "Finish")
        {
            Floor.material = Win;
            SetReward(1f);
            EndEpisode();
        }
    }
}
