using UnityEngine;

public class EnemyChaseState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        enemy.AnimationController.PlayWalkAnimation(true);
        enemy.TestSetStateText("Chase State", Color.red);
        enemy.SetMovement(enemy.Player.transform.position, enemy.EnemyData.ChaseSpeed);
    }

    public void ExitState(EnemyBase enemy)
    {

    }

    public void Tick(EnemyBase enemy)
    {
        //First Check attack distance and AttackState
        if (enemy.TargetInAttackRange())
            enemy.SwitchState(enemy.AttackState);

        //Second Check chaseDistance to go back WanderState
        if (!enemy.TargetInChaseDistance())
            enemy.SwitchState(enemy.WanderState);

        //Chasing to last point Then go back initial Position
        ChaseTarget(enemy);
    }
    private void ChaseTarget(EnemyBase enemy)
    {
        enemy.Agent.SetDestination(enemy.Player.transform.position);
    }

}
