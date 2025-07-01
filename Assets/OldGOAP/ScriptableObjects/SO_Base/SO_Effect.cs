using UnityEngine;

public abstract class SO_Effect : ScriptableObject
{
    public string EffectName;
    public abstract void ApplyEffect(GOAP_Agent agent,GameObject target);
}