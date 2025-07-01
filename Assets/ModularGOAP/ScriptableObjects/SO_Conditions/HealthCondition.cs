using UnityEngine;
 
 [CreateAssetMenu(fileName = "HealthCondition", menuName = "GOAP/Conditions/HealthCondition")]
 public class HealthCondition : SO_Condition
 {
     public enum HealthTargetType { Self, Target }
     public enum HealthComparisonType { IsAlive, BelowPercentage }
     //--------- add WasHit as another enum 
 
     public HealthTargetType healthTarget;
     public HealthComparisonType healthComparison;
     public float thresholdPercentage = 0.5f;
     public bool expectedValue = true;
 
     public override bool IsConditionMet(GOAP_Agent agent)
     {
         EnemyDataManager dataManager = null;
 
         if (healthTarget == HealthTargetType.Self)
         {
             dataManager = agent.GetComponent<EnemyDataManager>();
         }
         else if (healthTarget == HealthTargetType.Target && agent.Target != null)
         {
             dataManager = agent.Target.GetComponent<EnemyDataManager>();
         }
 
         if (dataManager == null) return false;
 
         bool result = false;
 
         switch (healthComparison)
         {
             case HealthComparisonType.IsAlive:
                 result = dataManager.IsAlive;
                 break;
             case HealthComparisonType.BelowPercentage:
                 float healthPercent = (float)dataManager.CurrentHealth / dataManager.MaxHealth;
                 result = healthPercent < thresholdPercentage;
                 break;
         }
 
         return result == expectedValue;
     }
 }
 