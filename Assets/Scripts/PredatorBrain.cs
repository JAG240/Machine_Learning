using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class PredatorBrain : Agent
{
    [SerializeField] private Transform prey;
    [SerializeField] private Transform preyHome;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private MeshRenderer Floor;
    [SerializeField] private Material Win;
    [SerializeField] private Material Lose;

    //Start a new episode
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(-3f, 0.75f, 0f);
        prey.localPosition = new Vector3(5f, 0.5f, 0f);
        preyHome.localPosition = new Vector3(Random.Range(-5f, -3f), 0.75f, Random.Range(-3f, 3f));
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
        sensor.AddObservation(prey.localPosition);
        sensor.AddObservation(preyHome.localPosition);
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
        if(other.tag == "Prey")
        {
            SetReward(1f);
            Floor.material = Win;
            EndEpisode();
        }
        else if(other.tag == "Respawn")
        {
            SetReward(-2f);
            EndEpisode();
        }
    }

    //This is called when the prey makes it home 
    public void PreyWin()
    {
        SetReward(-1f);
        Floor.material = Lose;
        EndEpisode();
    }
}
