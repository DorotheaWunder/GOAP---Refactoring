using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewGoal", menuName = "GOAP/Goal")]
public class SO_Goal : ScriptableObject
{
    public string GoalName;
    public List<SO_Condition> GoalConditions;
    public float PriorityValue;
    public List<SO_Action> GoalActions;
    
    public bool IsGoalAchieved(GOAP_Agent agent)
    {
        foreach (var Goalcondition in GoalConditions)
        {
            if (!Goalcondition.IsConditionMet(agent))
            {
                return false;
            }
        }
        return true;
    }
    
    public SO_Action GetGoalAction(GOAP_Agent agent, GOAP_Planner planner)
    {
        return planner.ChooseBestActionForGoal(agent, this, GoalActions);
    }
}