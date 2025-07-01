using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    [Header("Health Data")]
    public SO_EnemyData EnemyData;
    [SerializeField] private int currentHealth;
    public int MaxHealth => EnemyData != null ? EnemyData.MaxHealth : 0;
    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = Mathf.Clamp(value, 0, MaxHealth); 
    }
    
    public bool IsAlive => CurrentHealth > 0;
    public bool HasLowHealth => CurrentHealth <= MaxHealth * 0.5f;
    
    [Header("Combat Data")]
    [SerializeField] private float currentCooldown;
    public bool CooldownIsFinished => currentCooldown <= 0;
    [SerializeField] private float _enemyMovementSpeed = 3f;
    private ParticleSystem _bloodFX;
    
    public float CurrentSpeed
    {
        get => _enemyMovementSpeed;
        set => _enemyMovementSpeed = value; 
    }
    
    private void Awake()
    {
        if (EnemyData != null) { InitializeEnemyStats(); }
        else { Debug.LogWarning("EnemyData is not assigned in the EnemyDataManager!"); }
    }
    
    private void InitializeEnemyStats()
    {
        CurrentHealth = EnemyData.MaxHealth;
        CurrentSpeed = EnemyData.MovementSpeed;
    }
}
