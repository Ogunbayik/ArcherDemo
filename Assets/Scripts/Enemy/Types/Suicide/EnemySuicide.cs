using UnityEngine;

public class EnemySuicide : EnemyBase
{
    private SuicideDataSO SuicideData => EnemyData as SuicideDataSO;

    public override void OnChaseStart()
    {
        SwitchState(SuicideState);
    }
    public override bool TargetInAttackRange()
    {
        //Checking for melee and range Enemies
        return false;
    }
    public override float GetSuicideTime()
    {
        return SuicideData.SuicideTime;
    }
    public override AttackStrategySO AttackStrategy()
    {
        return null;
    }
    public override Transform GetAttackTransform()
    {
        return null;
    }
}
