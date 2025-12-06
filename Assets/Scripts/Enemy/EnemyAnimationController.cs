using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayAwarenessAnimation()
    {
        animator.SetTrigger("OnAwareness");
    }
    public void PlayWalkAnimation(bool isMove)
    {
        animator.SetBool("isWalk", isMove);
    }
    public void PlayExplodeAnimation()
    {
        animator.SetTrigger("OnExplode");
    }


}
