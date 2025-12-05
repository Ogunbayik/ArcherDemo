using UnityEngine;

public class EnemySuicide : EnemyBase
{
    private SuicideDataSO SuicideData => EnemyData as SuicideDataSO;

    public override void OnChaseStart()
    {
        Debug.Log("Suicide State");
        SwitchState(SelfDestructionState);
    }
    public override bool TargetInAttackRange()
    {
        //Checking for melee and range Enemies
        return false;
    }
    public override float GetSelfDestructionTime()
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
