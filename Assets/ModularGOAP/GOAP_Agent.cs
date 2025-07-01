using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class GOAP_Agent : MonoBehaviour
{
    [Header("Goap Planner")]
    private GOAP_Planner planner = new();
    public List<SO_Goal> availableGoals;
    public SO_Goal currentGoal;
    public SO_Action currentAction;
    public List<SO_Action> availableActions; 
    public List<SO_Condition> conditions;
    
    [Header("Enemy Data")]
    public SO_EnemyData enemyData;
    private EnemyDataManager _enemyDataManager;
    private EnemyVisionScript _enemyVision;
    
    [Header("Ally List")]
    public List<GameObject> NearbyAllies = new();
    public EnemyDataManager GetCharacterDataManager()
    {
        if (_enemyDataManager == null)
        {
            _enemyDataManager = GetComponent<EnemyDataManager>();
            if (_enemyDataManager == null)
            {
                _enemyDataManager = gameObject.AddComponent<EnemyDataManager>();
                _enemyDataManager.EnemyData = enemyData;
            }
        }
        return _enemyDataManager;
    }
    public bool IsAlive
    {
        get
        {
            var manager = GetComponent<EnemyDataManager>();
            return manager != null && manager.IsAlive;
        }
    }
    public bool HasLowHealth
    {
        get
        {
            var manager = GetComponent<EnemyDataManager>();
            return manager != null && manager.HasLowHealth;
        }
    }
    public bool TargetIsAlive
    {
        get
        {
            if (Target == null) return false;

            var targetManager = Target.GetComponent<EnemyDataManager>();
            return targetManager != null && targetManager.IsAlive;
        }
    }
    public bool TargetHasLowHealth
    {
        get
        {
            if (Target == null) return false;

            var targetManager = Target.GetComponent<EnemyDataManager>();
            return targetManager != null && targetManager.HasLowHealth;
        }
    }
    
    [Header("Enemy Movement")]
    public LayerMask targetLayer; 
    private NavMeshAgent agent;
    public GameObject Target;
    public Transform TargetTransform => Target?.transform;
    [SerializeField] public List<Transform> patrolPoints;
    public int currentPatrolIndex = 0;
    
    private void Awake()
    {
        _enemyVision = GetComponentInChildren<EnemyVisionScript>();
        if (_enemyVision == null)
        { Debug.LogError("EnemyVisionScript not found as a child of GOAP_Agent."); }
    }

    private void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        if (enemyData != null && GetCharacterDataManager().EnemyData != null)
        {
            agent.speed = _enemyDataManager.CurrentSpeed;
        }
    }
    
    private void Start()
    {
        StartCoroutine(UpdateRoutine());
    }
    
    private IEnumerator UpdateRoutine()
    {
        while (true)
        {
            OnUpdateEvent();
            yield return new WaitForSeconds(0.1f); // Adjust the delay as needed
        }
    }
    public void OnUpdateEvent()
    {
        ChooseAndExecuteAction();
    }
    
    public SO_Goal ChooseBestGoal() //---- what about goal conditions only
    {
        SO_Goal bestGoal = null;
        float highestPriority = float.MinValue;
        
        foreach (SO_Goal goal in availableGoals)
        {
            if (goal.IsGoalAchieved(this))
            {
                if (goal.PriorityValue > highestPriority)
                {
                    bestGoal = goal;
                    highestPriority = goal.PriorityValue;
                }
            }
        }
        Debug.LogWarning("Selected Goal Action: " + currentAction?.ActionName);
        return bestGoal;
    }
    
    void ChooseAndExecuteAction()
    {
        currentGoal = ChooseBestGoal();
        if (currentGoal != null)
        {
            SO_Action chosenAction = currentGoal.GetGoalAction(this, planner);
            if (chosenAction == null)
            {
                chosenAction = planner.ChooseBestAction(this, availableActions);
            }

            if (chosenAction != null)
            {
                currentAction = chosenAction;
                conditions = chosenAction.preconditions;
                bool actionPerformed = chosenAction.PerformAction(this);
                if (actionPerformed)
                {
                    Debug.LogWarning($"Action performed: {chosenAction.ActionName}");
                }
                else
                {
                    Debug.LogError($"Failed to perform action: {chosenAction.ActionName}");
                }
            }
        }
        Debug.LogWarning($"Selected Goal: {currentGoal?.GoalName}, Selected Action: {currentAction?.ActionName}");
    }
    
    void OnDrawGizmos()
    {
        if (enemyData == null)
            return;
        
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyData.DetectionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, enemyData.ShootingRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyData.MeleeRange);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(GOAP_Agent))]
public class GOAP_AgentEditor : Editor
{
    private GOAP_Agent agent;

    void OnEnable()
    {
        agent = (GOAP_Agent)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Current Action", EditorStyles.boldLabel);
        if (agent.currentAction != null)
        {
            EditorGUILayout.LabelField("Action Name: ", agent.currentAction.name);
        }
        else
        {
            EditorGUILayout.LabelField("No Action", "The agent is not performing any action.");
        }
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);
        if (agent.conditions != null && agent.conditions.Count > 0)
        {
            foreach (var condition in agent.conditions)
            {
                bool isConditionMet = condition.IsConditionMet(agent);
                EditorGUILayout.LabelField(condition.name, isConditionMet ? "FULFILLED" : " not fulfilled");
            }
        }
        else
        {
            EditorGUILayout.LabelField("No Conditions", "There are no conditions being checked.");
        } 
        serializedObject.ApplyModifiedProperties();
    }
    
}
#endif
