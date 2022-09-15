using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyHome : MonoBehaviour
{
    [SerializeField] PredatorBrain predatorBrain;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Prey")
        {
            predatorBrain.PreyWin();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Prey")
        {
            predatorBrain.PreyWin();
        }
    }
}
