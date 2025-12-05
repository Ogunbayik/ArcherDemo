using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemySelfDestructionState : IEnemyState
{
    private float destructionTimer;
    public void EnterState(EnemyBase enemy)
    {
        
        enemy.TestSetStateText("Self Destruction State", Color.black);
        enemy.SetMovement(enemy.Player.transform.position, enemy.EnemyData.ChaseSpeed);
        destructionTimer = enemy.GetSelfDestructionTime();
    }
    public void ExitState(EnemyBase enemy)
    {

    }
    public void Tick(EnemyBase enemy)
    {
        FollowPlayer(enemy);
        CountdownTimer(enemy);
    }
    private void FollowPlayer(EnemyBase enemy)
    {
        enemy.Agent.SetDestination(enemy.Player.transform.position);
    }
    private void CountdownTimer(EnemyBase enemy)
    {
        destructionTimer -= Time.deltaTime;

        if (destructionTimer <= 0)
        {
            StartDestructionSequnce(enemy);
            destructionTimer = 100f;
        }
    }
    private void StartDestructionSequnce(EnemyBase enemy)
    {
        Sequence destructionSequence = DOTween.Sequence();

        //Waiting Destruction Animation
        destructionSequence.AppendInterval(2f);
        destructionSequence.AppendCallback(() => Debug.Log("Activate SMOKE VFX"));
        destructionSequence.AppendInterval(1f);
        destructionSequence.AppendCallback(() => enemy.gameObject.SetActive(false));
        //Sent to object to Pool
    }

  
}
