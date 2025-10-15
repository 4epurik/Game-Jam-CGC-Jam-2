using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private string isRunningStr = "isRunning";
    private string isJumpingStr = "isJumping";
    private string isDashingStr = "isDashing";
    private string isDeadStr = "isDead";


    public void Initialize()
    {
        if (anim != null)
        {
            anim.SetBool(isRunningStr, false);
            anim.SetBool(isJumpingStr, false);
            anim.SetBool(isDashingStr, false);
            anim.SetBool(isDeadStr, false);
        }
    }

    public void SetRunning(bool isRunning)
    {
        if (anim != null)
            anim.SetBool(isRunningStr, isRunning);
    }

    public void SetJumping(bool isJumping)
    {
        if (anim != null)
        {
            anim.SetBool(isRunningStr, !isJumping);
            anim.SetBool(isJumpingStr, isJumping);
        }
    }

    public void SetDashing(bool isDashing)
    {
        if (anim != null)
            anim.SetBool(isDashingStr, isDashing);
    }

    public void SetDead()
    {
        if (anim != null)
        {
            anim.SetBool(isRunningStr, false);
            anim.SetBool(isJumpingStr, false);
            anim.SetBool(isDashingStr, false);
            anim.SetBool(isDeadStr, true);
        }
    }
}