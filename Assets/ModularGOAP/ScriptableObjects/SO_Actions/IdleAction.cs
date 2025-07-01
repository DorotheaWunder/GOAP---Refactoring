using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleAction", menuName = "GOAP/Actions/Idle Action")]
public class IdleAction : SO_Action
{
    public override bool PerformAction(GOAP_Agent agent)
    {
        UnityEngine.AI.NavMeshAgent navMeshAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent != null)
        {
            Debug.Log("Enemy is being idle");
            navMeshAgent.ResetPath();
            return true;
        }
        return false;
    }
}

