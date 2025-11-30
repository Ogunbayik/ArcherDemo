using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [Header("Attack Settings")]
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackRate;
    [Header("Wander Settings")]
    [SerializeField] private float _wanderDistance;
    [SerializeField] private float _wanderSpeed;
    [Header("Chase Settings")]
    [SerializeField] private float _chaseDistance;
    [SerializeField] private float _chaseSpeed;


    public string Name => _enemyName;
    public float WanderDistance => _wanderDistance;
    public float WanderSpeed => _wanderSpeed;
    public float AttackDistance => _attackDistance;
    public float AttackRate => _attackRate;
    public float ChaseDistance => _chaseDistance;
    public float ChaseSpeed => _chaseSpeed;
}
