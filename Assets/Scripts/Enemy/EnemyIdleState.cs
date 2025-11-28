using UnityEngine;

public class EnemyIdleState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        Debug.Log(enemy.EnemySO.Name + " is Spawned with Idle");
    }

    public void ExitState(EnemyBase enemy)
    {
        
    }

    public void Tick(EnemyBase enemy)
    {
        
    }
}
