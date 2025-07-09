using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "GOAP/EnemyData")]
public class SO_EnemyData : ScriptableObject
{
    [Header("Enemy Data")]
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = Mathf.Max(1, value);
    }
    public int CurrentHealth
    {
        get => _currentHealth;
        private set => _currentHealth = Mathf.Clamp(value, 0, MaxHealth);
    }
    
    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
    }
    
    [Header("Vision Spheres")]
    [SerializeField] private float _communicationRange = 70.0f;
    [SerializeField] private float _detectionRange = 20.0f;
    [SerializeField] private float _shootingRange = 10.0f;
    [SerializeField] private float _meleeRange = 5.0f;

    public float CommunicationRange => _communicationRange;
    public float DetectionRange => _detectionRange;
    public float ShootingRange => _shootingRange;
    public float MeleeRange => _meleeRange;
    
    [Header("Cooldowns")]
    [SerializeField] private float _actionCooldown;
    public float ActionCooldown => _actionCooldown;
    
    [Header("Combat")]
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private float _meleeDamage = 10f;
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private bool _hasRangedWeapon;
    [SerializeField] private GameObject _weapon;

    public float MovementSpeed => _movementSpeed;
    public float MeleeDamage => _meleeDamage;
    public float AttackCooldown => _attackCooldown;
    public bool HasRangedWeapon => _hasRangedWeapon;
    public GameObject Weapon => _weapon;//------- maybe have the ranged on weapon and weapon object with SO?
    
    public void InitializeDefaults()
    {
        ResetHealth();
    }
}
