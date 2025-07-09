using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveToAction", menuName = "GOAP/Actions/MoveToAction")]
public class MoveToAction : SO_Action
{
    public override bool PerformAction(GOAP_Agent agent)
    {
        if (agent.Target == null) return false;

        UnityEngine.AI.NavMeshAgent navMeshAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = agent.enemyData.MovementSpeed;
            navMeshAgent.SetDestination(agent.Target.transform.position);
            return true;
        }
        return false;
    }
}
