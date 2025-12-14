using UnityEngine;

public abstract class BaseEnemyDataSO : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [Header("Wander Settings")]
    [SerializeField] private float _wanderDistance;
    [SerializeField] private float _wanderSpeed;
    [Header("Chase Settings")]
    [SerializeField] private float _chaseDistance;
    [SerializeField] private float _chaseSpeed;

    public string Name => _enemyName;
    public float WanderDistance => _wanderDistance;
    public float WanderSpeed => _wanderSpeed;
    public float ChaseDistance => _chaseDistance;
    public float ChaseSpeed => _chaseSpeed;
}
