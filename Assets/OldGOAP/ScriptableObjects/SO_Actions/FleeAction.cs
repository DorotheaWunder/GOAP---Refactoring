using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FleeAction", menuName = "GOAP/Actions/FleeAction")]
public class FleeAction : SO_Action
{
    public override bool PerformAction(GOAP_Agent agent)
    {
        
        if (agent.Target == null)
        {
            Debug.LogWarning("No target assigned for FleeAction.");
            return false; // No target, can't perform action
        }
        
        if (agent.Target.transform.position == null)
        {
            Debug.LogWarning("Target has no position.");
            return false; // Target's position is invalid
        }
        
        UnityEngine.AI.NavMeshAgent navMeshAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent != null)
        {
            Debug.Log("Enemy is fleeing");
            Vector3 playerDirection = agent.Target.transform.position - agent.transform.position;
            Vector3 oppositeDirection = agent.transform.position - playerDirection;
            navMeshAgent.SetDestination(oppositeDirection);
            
            return true;
        }
        return false;
    }
}

