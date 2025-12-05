using UnityEngine;

[CreateAssetMenu(fileName = "New Range Enemy", menuName = "ScriptableObject/Enemy/Range Enemy")]
public class RangeDataSO : BaseEnemyDataSO
{
    [Header("Attack Settings")]
    [SerializeField] private float _attackDistance;
    [SerializeField] private AttackStrategySO _attackStrategy;

    public float AttackDistance => _attackDistance;
    public AttackStrategySO AttackStrategy => _attackStrategy;
}
