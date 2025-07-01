using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveTargetEffect", menuName = "GOAP/Effects/MoveTargetEffect")]
public class MoveTargetEffect : SO_Effect
{
    public bool moveTowardTarget = true;

    public override void ApplyEffect(GOAP_Agent agent, GameObject target)
    {
        if (target == null || agent == null) return;
        
        UnityEngine.AI.NavMeshAgent navMeshAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent != null)
        {
            Vector3 targetPosition = target.transform.position;

            if (!moveTowardTarget)
            {
                Vector3 direction = agent.transform.position - targetPosition;
                targetPosition = agent.transform.position + direction;
            }
            
            navMeshAgent.SetDestination(targetPosition);
            navMeshAgent.speed = agent.enemyData.MovementSpeed;
        }
    }
}
