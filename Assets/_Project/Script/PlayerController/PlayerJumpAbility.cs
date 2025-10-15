using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpAbility : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float jumpForce = 9f;
    [SerializeField] private int maxJumps = 2;
    
    private int jumpCount;
    private AnimationController animationController;

    private MovementData movementData;


    public bool IsJumping => jumpCount > 0; // Прыжок считается активным, пока count > 0

    public void Initialize(MovementData movementData, AnimationController anim)
    {
        this.animationController = anim;
        this.movementData = movementData;
    }

    public void TryJump()
    {
        if (jumpCount < maxJumps)
        {
            Jump();
        }
    }

    private void Jump()
    {
        movementData.Dir.y = jumpForce;
        jumpCount++;
        animationController?.SetJumping(true);
    }

    public void ResetJumps()
    {
        if (jumpCount != 0)
        {
            jumpCount = 0;
            animationController?.SetJumping(false);
        }
    }

    // Метод для улучшения силы прыжка, если это нужно
    public void IncreaseJump(int amount) { jumpForce += amount; }
}