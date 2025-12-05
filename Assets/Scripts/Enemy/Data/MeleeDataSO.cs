using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Enemy", menuName = "ScriptableObject/Enemy/Melee")]
public class MeleeDataSO : BaseEnemyDataSO
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackTransform;
    [SerializeField] private AttackStrategySO _attackStrategy;
    

    public float AttackDistance => _attackDistance;
    public float AttackTransform => _attackTransform;
    public AttackStrategySO AttackStrategy => _attackStrategy;
}
