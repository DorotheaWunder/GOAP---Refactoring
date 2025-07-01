using System.Collections.Generic;

public class GOAP_Planner 
{
    public SO_Action ChooseBestActionForGoal(GOAP_Agent agent, SO_Goal goal, List<SO_Action> availableActions)
    {
        SO_Action bestAction = null;
        float lowestCost = float.MaxValue;
    
        foreach (var action in goal.GoalActions)
        {
            if (action.AreConditionsMet(agent))
            {
                if (action.ActionPriority < lowestCost)
                {
                    bestAction = action;
                    lowestCost = action.ActionPriority;
                }
            }
        }
        return bestAction;
    }
    
    public SO_Action ChooseBestAction(GOAP_Agent agent, List<SO_Action> availableActions)
    {
        SO_Action bestAction = null;
        float lowestCost = float.MaxValue;

        foreach (var action in availableActions)
        {
            if (action.AreConditionsMet(agent))
            {
                if (action.ActionPriority < lowestCost)
                {
                    bestAction = action;
                    lowestCost = action.ActionPriority;
                }
            }
        }
        
        return bestAction;
    }
}
