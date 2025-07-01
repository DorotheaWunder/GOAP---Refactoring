using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolAction", menuName = "GOAP/Actions/PatrolAction")]
public class PatrolAction : SO_Action
{
    public override bool PerformAction(GOAP_Agent agent)
    {
        if (agent.patrolPoints == null || agent.patrolPoints.Count == 0) return false;
        UnityEngine.AI.NavMeshAgent navMeshAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent == null) return false;


        navMeshAgent.speed = agent.enemyData.MovementSpeed;
        
        Transform currentPatrolPoint = agent.patrolPoints[agent.currentPatrolIndex];
        navMeshAgent.SetDestination(currentPatrolPoint.position);

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            agent.currentPatrolIndex = (agent.currentPatrolIndex +1) % agent.patrolPoints.Count; 
        }

        return true;
    }
}

