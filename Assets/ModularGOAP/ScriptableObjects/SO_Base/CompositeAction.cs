using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompositeAction", menuName = "GOAP/Actions/CompositeAction")]
public class CompositeAction : SO_Action
{
    public List<SO_Action> subActions = new List<SO_Action>();

    public override bool PerformAction(GOAP_Agent agent)
    {
        foreach (var action in subActions)
        {
            if (action.AreConditionsMet(agent))
            {
                return action.PerformAction(agent);
            }
        }
        return false; 
    }
}
