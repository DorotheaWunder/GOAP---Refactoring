using UnityEngine;

[CreateAssetMenu(menuName = "GOAP/Conditions/Negated Condition")]
public class SO_NotCondition : SO_Condition
{
    [SerializeField]
    private SO_Condition originalCondition;

    public override bool IsConditionMet(GOAP_Agent agent)
    {
        return !originalCondition.IsConditionMet(agent);
    }
}
