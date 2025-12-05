using UnityEngine;

public class EnemyMelee : EnemyBase
{
    private MeleeDataSO meleeData => EnemyData as MeleeDataSO;


    public override void OnChaseStart()
    {
        SwitchState(ChaseState);
    }
    public override float GetSelfDestructionTime()
    {
        return 0f;
    }
    public override bool TargetInAttackRange()
    {
        return GetDistanceBetweenPlayer() <= meleeData.AttackDistance;
    }
    public override AttackStrategySO AttackStrategy()
    {
        return meleeData.AttackStrategy;
    }
    public override Transform GetAttackTransform()
    {
        return null;
    }
}
