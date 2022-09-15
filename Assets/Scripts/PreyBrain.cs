using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PreyBrain : Agent
{
    [SerializeField] private GameObject Predator;
    [SerializeField] private Transform Home;
    [SerializeField] private MeshRenderer Floor;
    [SerializeField] private Material PreyWin;
    [SerializeField] private Material PredatorWin;
    [SerializeField] private float moveSpeed = 1f;

    //Start a new episode
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(5f, 0.5f, 0f);
        Predator.transform.localPosition = new Vector3(-3f, 0.75f, 0f);
        Home.localPosition = new Vector3(Random.Range(-5f, -3f), 0.75f, Random.Range(-3f, 3f));
    }

    //when an action is called
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    //Observations collected
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(Predator.transform.localPosition);
        sensor.AddObservation(Home.localPosition);
    }

    //Manualy controls
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    //Used to train the brain
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Respawn")
        {
            SetReward(-2f);
            EndEpisode();
        }
        else if(other.tag == "Predator")
        {
            SetReward(-1f);
            Floor.material = PredatorWin;
            EndEpisode();
        }
        else if(other.tag == "Finish")
        {
            SetReward(1f);
            Floor.material = PreyWin;
            EndEpisode();
        }
    }
}
