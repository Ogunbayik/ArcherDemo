using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        enemy.SetStateName("Chase State", Color.red);
        enemy.SetMovement(enemy.TargetPlayer.transform.position, enemy.EnemySO.ChaseSpeed);
    }

    public void ExitState(EnemyBase enemy)
    {

    }

    public void Tick(EnemyBase enemy)
    {
        //First Check attack distance and AttackState
        if (enemy.GetDistanceBetweenPlayer() <= enemy.EnemySO.AttackDistance)
            enemy.SwitchState(enemy.AttackState);

        //Second Check chaseDistance to go back WanderState
        if (enemy.GetDistanceBetweenPlayer() >= enemy.EnemySO.ChaseDistance)
            enemy.SwitchState(enemy.WanderState);

        //Chasing to last point Then go back initial Position
        ChaseTarget(enemy);
    }
    private void ChaseTarget(EnemyBase enemy)
    {
        enemy.Agent.SetDestination(enemy.TargetPlayer.transform.position);
    }

}
