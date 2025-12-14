using UnityEngine;
using DG.Tweening;

public class EnemyAttackState : IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        //Create HandleAttackSequnce
        enemy.TestSetStateText("Attack State", Color.darkRed);

        if (enemy.CanAttack())
        {
            HandleAttackSequence(enemy);
            //enemy.AttackStrategy().ExecuteAttack(enemy);
            //enemy.SetCurrentCooldown(enemy.AttackStrategy().AttackCooldown);
        }
    }

    public void ExitState(EnemyBase enemy)
    {
        enemy.Agent.isStopped = false;
    }

    public void Tick(EnemyBase enemy)
    {
        RotateEnemy(enemy);

        if (!enemy.CanAttack())
            enemy.SwitchState(enemy.AttackWaitState);
    }

    private void RotateEnemy(EnemyBase enemy)
    {
        var lookRotation = enemy.Player.transform.position - enemy.transform.position;
        lookRotation.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(lookRotation);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, 150f * Time.deltaTime);
    }
    
    private void HandleAttackSequence(EnemyBase enemy)
    {
        Sequence attackSequence = DOTween.Sequence();
        //First Section Activate Animation
        attackSequence.AppendCallback(() => enemy.StoppedCharacter());
        attackSequence.JoinCallback(() => enemy.AnimationController.PlayAttackAnimation());
        attackSequence.JoinCallback(() => enemy.transform.LookAt(enemy.Player.transform.position));
        //Second Part Create Bullet
        attackSequence.AppendInterval(2.875f);
        attackSequence.AppendCallback(() => enemy.AttackStrategy().ExecuteAttack(enemy));
        attackSequence.JoinCallback(() => enemy.SetCurrentCooldown(enemy.AttackStrategy().AttackCooldown));
        attackSequence.JoinCallback(() => enemy.SwitchState(enemy.AttackWaitState));
        //Last Part

    }
}
