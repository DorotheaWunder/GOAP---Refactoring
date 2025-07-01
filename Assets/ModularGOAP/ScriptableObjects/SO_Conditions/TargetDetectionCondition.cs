using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetDetectionCondition", menuName = "GOAP/Conditions/TargetDetectionCondition")]
public class TargetDetectionCondition : SO_Condition
{
    public string requiredTag;
    public LayerMask targetLayer;
    public RangeType detectionRange = RangeType.DetectionRange;

    public enum RangeType
    {
        DetectionRange,
        ShootingRange,
        MeleeRange, 
        CommunicationRange
    }

    public override bool IsConditionMet(GOAP_Agent agent)
    {
        float range = GetRange(agent);

        float stepAngle = 10f;
        for (float i = -180f; i <= 180f; i += stepAngle)
        {
            Vector3 direction = Quaternion.Euler(0, i, 0) * agent.transform.forward;
            if (Physics.Raycast(agent.transform.position, direction, out RaycastHit hit, range, targetLayer))
            {
                if (hit.transform.CompareTag(requiredTag))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private float GetRange(GOAP_Agent agent)
    {
        return detectionRange switch
        {
            RangeType.MeleeRange => agent.enemyData.MeleeRange,
            RangeType.ShootingRange => agent.enemyData.ShootingRange,
            RangeType.DetectionRange => agent.enemyData.DetectionRange,
            RangeType.CommunicationRange => agent.enemyData.CommunicationRange,
            _ => 0f
        };
    }
}
