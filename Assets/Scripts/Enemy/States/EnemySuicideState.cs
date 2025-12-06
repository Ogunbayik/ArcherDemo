using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemySuicideState : IEnemyState
{
    private float awarenessTime = 2.042f;
    private float explodeTime = 2.25f;

    private float suicideTimer;

    private bool isSuicide = false;
    private bool canFollow = false;
    public void EnterState(EnemyBase enemy)
    {
        enemy.StoppedCharacter();
        enemy.TestSetStateText("Awaness", Color.black);
        StartAwarenessSequence(enemy);
    }
    private void StartAwarenessSequence(EnemyBase enemy)
    {
        Sequence awarenessSequnce = DOTween.Sequence();

        //First Part Play animation
        awarenessSequnce.AppendCallback(() => enemy.AnimationController.PlayAwarenessAnimation());
        awarenessSequnce.AppendInterval(awarenessTime);
        //Second Part Set move position and activate destructionTimer
        awarenessSequnce.AppendCallback(() => enemy.SetMovement(enemy.Player.transform.position, enemy.EnemyData.ChaseSpeed));
        awarenessSequnce.JoinCallback(() => StartFollow(true));
        awarenessSequnce.JoinCallback(() => enemy.AnimationController.PlayWalkAnimation(true));
        awarenessSequnce.JoinCallback(() => StartDestructionCountDown(enemy));
        awarenessSequnce.JoinCallback(() => enemy.TestSetStateText("Suicide State", Color.black));
    }
    public void ExitState(EnemyBase enemy)
    {

    }
    public void Tick(EnemyBase enemy)
    {
        if (canFollow)
            FollowPlayer(enemy);

        if (isSuicide)
            CountdownTimer(enemy);
    }
    private void FollowPlayer(EnemyBase enemy)
    {
        enemy.Agent.SetDestination(enemy.Player.transform.position);
    }
    private void CountdownTimer(EnemyBase enemy)
    {
        suicideTimer -= Time.deltaTime;

        if (suicideTimer <= 0)
        {
            StartDestructionSequnce(enemy);
            suicideTimer = 100f;
        }
    }
    private void StartDestructionSequnce(EnemyBase enemy)
    {
        Sequence destructionSequence = DOTween.Sequence();

        //First Part Waiting Destruction Animation
        destructionSequence.AppendCallback(() => enemy.StoppedCharacter());
        destructionSequence.JoinCallback(() => StartFollow(false));
        destructionSequence.JoinCallback(() => enemy.AnimationController.PlayExplodeAnimation());
        //Second Part Create Smoke and Destroy Object and CheckRadius for damage
        destructionSequence.AppendInterval(explodeTime);
        destructionSequence.AppendCallback(() => Debug.Log("Activate SMOKE VFX"));
        destructionSequence.AppendCallback(() => enemy.gameObject.SetActive(false));
        //Sent to object to Pool
    }
    private void StartDestructionCountDown(EnemyBase enemy)
    {
        suicideTimer = enemy.GetSuicideTime();
        isSuicide = true;
    }
    private void StartFollow(bool isActive)
    {
        canFollow = isActive;
    }

  
}
