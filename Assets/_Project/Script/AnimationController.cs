using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public void Initialize()
    {
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isDashing", false);
            anim.SetBool("isDead", false);
        }
    }

    public void SetRunning(bool isRunning)
    {
        if (anim != null)
            anim.SetBool("isRunning", isRunning);
    }

    public void SetJumping(bool isJumping)
    {
        if (anim != null)
        {
            anim.SetBool("isRunning", !isJumping);
            anim.SetBool("isJumping", isJumping);
        }
    }

    public void SetDashing(bool isDashing)
    {
        if (anim != null)
            anim.SetBool("isDashing", isDashing);
    }

    public void SetDead()
    {
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isDashing", false);
            anim.SetBool("isDead", true);
        }
    }
}