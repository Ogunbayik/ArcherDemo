using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        //Create HandleAttackSequnce
        enemy.Agent.isStopped = true;
        enemy.transform.LookAt(enemy.Player.transform.position);
        enemy.SetStateName("Attack State", Color.darkRed);

        if (enemy.CanAttack())
        {
            enemy.CurrentAttackSO.ExecuteAttack(enemy);
            enemy.SetCurrentCooldown(enemy.CurrentAttackSO.AttackCooldown);
        }
    }

    public void ExitState(EnemyBase enemy)
    {
        enemy.Agent.isStopped = false;
    }

    public void Tick(EnemyBase enemy)
    {
        enemy.transform.LookAt(enemy.Player.transform);

        if (!enemy.TargetInAttackRange())
            enemy.SwitchState(enemy.ChaseState);

        if (enemy.CanAttack())
        {
            enemy.CurrentAttackSO.ExecuteAttack(enemy);
            enemy.SetCurrentCooldown(enemy.CurrentAttackSO.AttackCooldown);
        }
    }
}
