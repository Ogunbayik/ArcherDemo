using UnityEngine;

public class EnemyAttackState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        //Create HandleAttackSequnce
        enemy.Agent.isStopped = true;
        enemy.transform.LookAt(enemy.TargetPlayer.transform.position);
        enemy.SetStateName("Attack State", Color.darkRed);
        Debug.Log("Player is Attacking to Player now!");
        enemy.CurrentAttackSO.ExecuteAttack(enemy);
    }

    public void ExitState(EnemyBase enemy)
    {
        enemy.Agent.isStopped = false;
    }

    public void Tick(EnemyBase enemy)
    {
        if (enemy.GetDistanceBetweenPlayer() > enemy.EnemySO.AttackDistance)
            enemy.SwitchState(enemy.ChaseState);
    }
}
