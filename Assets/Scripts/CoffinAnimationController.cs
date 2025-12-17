using UnityEngine;

public class CoffinAnimationController : MonoBehaviour
{
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void PlaySummonAnimation()
    {
        animator.SetTrigger("OnSummon");
    }
    public void PlayOpenAnimation()
    {
        animator.SetTrigger("OnOpen");
    }
    public void PlayDestroyAnimation()
    {
        Destroy(this.gameObject);
    }
}
