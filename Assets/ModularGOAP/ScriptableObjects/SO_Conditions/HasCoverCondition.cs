using UnityEngine;

[CreateAssetMenu(fileName = "HasCoverCondition", menuName = "GOAP/Conditions/HasCoverCondition")]
public class HasCoverCondition : SO_Condition
{
    public enum CoverCheckTarget { Self, Target }
    public CoverCheckTarget checkTarget = CoverCheckTarget.Self;
    public bool expectedResult = true;

    public LayerMask obstructionLayer;

    public override bool IsConditionMet(GOAP_Agent agent)
    {
        Transform source, target;

        if (checkTarget == CoverCheckTarget.Self)
        {
            target = agent.Target?.transform;
            source = agent.transform;
        }
        else
        {
            target = agent.Target?.transform;
            source = agent.transform;
        }

        if (target == null)
            return false;

        Vector3 direction = target.position - source.position;
        float distance = direction.magnitude;
        direction.Normalize();

        bool isBlocked = Physics.Raycast(source.position, direction, distance, obstructionLayer);
        
        return isBlocked == expectedResult;
    }
}
