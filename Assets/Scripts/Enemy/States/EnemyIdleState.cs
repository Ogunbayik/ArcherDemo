using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float startMoveTime = 2.458f;

    private float moveTimer;
    public void EnterState(EnemyBase enemy)
    {
        enemy.AnimationController.PlayWalkAnimation(false);
        moveTimer = startMoveTime;
        enemy.TestSetStateText("Idle State", Color.darkSeaGreen);
    }

    public void ExitState(EnemyBase enemy)
    {
        
    }

    public void Tick(EnemyBase enemy)
    {
        moveTimer -= Time.deltaTime;

        if (enemy.TargetInChaseDistance())
            enemy.OnChaseStart();

        if (moveTimer <= 0)
            enemy.SwitchState(enemy.WanderState);
    }
}
