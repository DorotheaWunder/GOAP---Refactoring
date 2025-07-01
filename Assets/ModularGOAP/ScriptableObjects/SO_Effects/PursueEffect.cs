using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Effects/PursueEffect")]
public class PursueEffect : SO_Effect
{
    public float stoppingDistance = 1f;

    public override void ApplyEffect(GOAP_Agent agent, GameObject target)
    {
        if (target == null || agent == null) return;

        var nav = agent.GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav == null) return;

        nav.stoppingDistance = stoppingDistance;
        nav.SetDestination(target.transform.position);
    }
}
