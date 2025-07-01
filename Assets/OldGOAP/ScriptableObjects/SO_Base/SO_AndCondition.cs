using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Conditions/And Condition")]
public class SO_AndCondition : SO_Condition
{
    [SerializeField]
    private List<SO_Condition> subConditions;

    public override bool IsConditionMet(GOAP_Agent agent)
    {
        foreach (var condition in subConditions)
        {
            if (!condition.IsConditionMet(agent))
                return false;
        }
        return true;
    }
}
