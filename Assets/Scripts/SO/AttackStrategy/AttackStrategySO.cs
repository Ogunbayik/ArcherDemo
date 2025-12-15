using UnityEngine;

public abstract class AttackStrategySO : ScriptableObject, IAttackStrategy
{
    [Header("Attack Settings")]
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float _attackCooldown;

    public float AttackCooldown => _attackCooldown;

    public abstract void ExecuteAttack(EnemyBase enemy);

}
