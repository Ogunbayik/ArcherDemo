using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private string _enemyName;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackRate;


    public string Name => _enemyName;
    public float MovementSpeed => _movementSpeed;
    public float AttackDistance => _attackDistance;
    public float AttackRate => _attackRate;
}
