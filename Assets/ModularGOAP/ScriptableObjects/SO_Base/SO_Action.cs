using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public abstract class SO_Action : ScriptableObject
{
    public string ActionName;
    public float ActionPriority;
    public List<SO_Condition> preconditions;
    public SO_Effect actionEffect;
    
    protected bool isInProgress = false;
    protected float lastActionTime = 0f;
    
    public virtual bool AreConditionsMet(GOAP_Agent agent)
    {
        foreach (SO_Condition condition in preconditions)
        {
            if (!condition.IsConditionMet(agent))
            {
                return false; 
            }
        }
        return true; 
    }

    public virtual bool PerformAction(GOAP_Agent agent)
    {
        isInProgress = true;

        if (actionEffect != null)
        {
            actionEffect.ApplyEffect(agent, agent.Target);
            isInProgress = false;
            return true;
        }

        Debug.LogWarning($"Action '{ActionName}' has no effect assigned.");
        isInProgress = false;
        return false;
    }
}