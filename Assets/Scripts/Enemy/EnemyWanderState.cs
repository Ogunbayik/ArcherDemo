using UnityEngine;
using UnityEngine.AI;

public class EnemyWanderState : IEnemyState
{
    private int maxAttempts = 10;
    private Vector3 wanderPosition;
    public void EnterState(EnemyBase enemy)
    {
        enemy.SetStateName("Wander State", Color.darkGreen);

        wanderPosition = GetRandomPosition(enemy);
        enemy.SetMovement(wanderPosition, enemy.EnemySO.WanderSpeed);
    }
    public void ExitState(EnemyBase enemy)
    {
        
    }
    public void Tick(EnemyBase enemy)
    {
        if (enemy.TargetInChaseDistance())
            enemy.SwitchState(enemy.ChaseState);

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
            var randomPosition = Random.insideUnitSphere * enemy.EnemySO.WanderDistance;
            randomPosition += enemy.InitialPosition;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPosition, out hit, enemy.EnemySO.WanderDistance, NavMesh.AllAreas))
                return hit.position;
        }

        return enemy.InitialPosition;
    }

}
