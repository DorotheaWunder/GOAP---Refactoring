using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Effects/FleeEffect")]
public class FleeEffect : SO_Effect
{
    public override void ApplyEffect(GOAP_Agent agent, GameObject target)
    {
        if (target == null) return;

        var nav = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav == null) return;

        Vector3 direction = agent.transform.position - target.transform.position;
        Vector3 fleePosition = agent.transform.position + direction.normalized * 10f;

        nav.SetDestination(fleePosition);
    }
}
