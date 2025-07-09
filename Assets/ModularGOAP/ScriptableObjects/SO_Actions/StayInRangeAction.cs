using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StayInRangeAction", menuName = "GOAP/Actions/StayInRangeAction")]
public class StayInRangeAction : SO_Action
{
    public float DesiredRange = 5f;
    
    public override bool PerformAction(GOAP_Agent agent)
    {
        if (agent.Target == null) return false;
        
        UnityEngine.AI.NavMeshAgent navMeshAgent = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent != null)
        {
            float currentDistance = Vector3.Distance(agent.transform.position, agent.Target.transform.position);

            if (currentDistance > DesiredRange)
            {
                Vector3 direction = (agent.Target.transform.position - agent.transform.position).normalized;
                Vector3 targetPosition = agent.Target.transform.position - direction * DesiredRange;
                navMeshAgent.SetDestination(targetPosition);
            }
            else if (currentDistance < DesiredRange)
            {
                Vector3 direction = (agent.transform.position - agent.Target.transform.position).normalized;
                Vector3 targetPosition = agent.Target.transform.position + direction * DesiredRange;
                navMeshAgent.SetDestination(targetPosition);
            }
            navMeshAgent.speed = agent.enemyData.MovementSpeed;
            return true;
        }
        return false;
    }
}