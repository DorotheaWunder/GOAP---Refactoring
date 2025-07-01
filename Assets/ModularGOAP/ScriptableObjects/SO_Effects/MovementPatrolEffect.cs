using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "PatrolEffect", menuName = "GOAP/Effects/PatrolEffect")]
public class MovementPatrolEffect : SO_Effect
{
    public float movementSpeed; 

    public override void ApplyEffect(GOAP_Agent agent, GameObject target)
    {
        if (target != null)
        {
            Transform patrolPoint = target.transform;
            MoveToPatrolPoint(agent, patrolPoint);
        }
    }
    
    public void MoveToPatrolPoint(GOAP_Agent agent, Transform patrolPoint)
    {
        NavMeshAgent navMeshAgent = agent.GetComponent<NavMeshAgent>();
        if (navMeshAgent != null && patrolPoint != null)
        {
            navMeshAgent.speed = agent.enemyData.MovementSpeed;
            navMeshAgent.SetDestination(patrolPoint.position);
        }
    }
}
