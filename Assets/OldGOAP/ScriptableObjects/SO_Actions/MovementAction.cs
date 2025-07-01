using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Actions/MovementAction")]
public class MovementAction : SO_Action
{
    public override bool PerformAction(GOAP_Agent agent)
    {
        if (!AreConditionsMet(agent)) return false;

        if (actionEffect != null)
        {
            actionEffect.ApplyEffect(agent, agent.Target);
            return true;
        }

        Debug.LogWarning("No effect assigned to movement action.");
        return false;
    }
}
