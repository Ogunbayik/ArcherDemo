using UnityEngine;

public class EnemyRange : EnemyBase
{
    [Header("Transform Settings")]
    [SerializeField] private Transform _attackTransform;
    private RangeDataSO RangeData => EnemyData as RangeDataSO;


    public override void OnChaseStart()
    {
        SwitchState(ChaseState);
    }
    public override float GetSuicideTime()
    {
        return 0f;
    }
    public override bool TargetInAttackRange()
    {
        return GetDistanceBetweenPlayer() <= RangeData.AttackDistance;
    }
    public override AttackStrategySO AttackStrategy()
    {
        return RangeData.AttackStrategy;
    }
    public override Transform GetAttackTransform()
    {
        return _attackTransform;
    }
}
