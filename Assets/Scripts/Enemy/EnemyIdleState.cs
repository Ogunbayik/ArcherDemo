using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    private float startMoveTime = 3f;

    private float moveTimer;
    public void EnterState(EnemyBase enemy)
    {
        moveTimer = startMoveTime;
        enemy.TestSetStateText("Idle State", Color.darkSeaGreen);
    }

    public void ExitState(EnemyBase enemy)
    {
        
    }

    public void Tick(EnemyBase enemy)
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
            enemy.SwitchState(enemy.WanderState);
    }
}
