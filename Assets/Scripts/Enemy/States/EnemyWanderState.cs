using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : IEnemyState
{
    private int maxAttempts = 10;
    private Vector3 wanderPosition;
    public void EnterState(EnemyBase enemy)
    {
        enemy.TestSetStateText("Wander State", Color.darkGreen);

        enemy.AnimationController.PlayWalkAnimation(true);
        wanderPosition = GetRandomPosition(enemy);
        enemy.SetMovement(wanderPosition, enemy.EnemyData.WanderSpeed);
    }
    public void ExitState(EnemyBase enemy)
    {
        
    }
    public void Tick(EnemyBase enemy)
    {
        if (enemy.TargetInChaseDistance())
            enemy.OnChaseStart();

        if(!enemy.Agent.pathPending && enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
        {
            if (!enemy.Agent.hasPath && enemy.Agent.velocity.sqrMagnitude == 0f)
                enemy.SwitchState(enemy.IdleState);
        }
    }
    private Vector3 GetRandomPosition(EnemyBase enemy)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            var randomPosition = Random.insideUnitSphere * enemy.EnemyData.WanderDistance;
            randomPosition += enemy.InitialPosition;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPosition, out hit, enemy.EnemyData.WanderDistance, NavMesh.AllAreas))
                return hit.position;
        }

        return enemy.InitialPosition;
    }

}
