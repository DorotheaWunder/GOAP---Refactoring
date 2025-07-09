using UnityEngine;

public abstract class SO_Condition : ScriptableObject
{
    public string ConditionName;
    public abstract bool IsConditionMet(GOAP_Agent agent);
}
