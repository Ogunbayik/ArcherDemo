using UnityEngine;

public class EnemyAttackWaitState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        enemy.TestSetStateText("Attack Waiting", Color.green);
    }

    public void ExitState(EnemyBase enemy)
    {

    }

    public void Tick(EnemyBase enemy)
    {
        if (enemy.CanAttack())
            enemy.SwitchState(enemy.AttackState);

        if (!enemy.TargetInAttackRange())
            enemy.SwitchState(enemy.ChaseState);
    }
}
