using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashAbility : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;

    private MovementData movementData;
    private AnimationController animationController;
    private float dashTimer;
    private float dashCooldownTimer;
    
    public bool IsDashing { get; private set; }

    public void Initialize(MovementData movementData, AnimationController anim)
    {
        this.movementData = movementData;
        this.animationController = anim;
    }

    public void TryDash()
    {
        if (dashCooldownTimer <= 0f && !IsDashing)
        {
            StartDash();
        }
    }
    
    public void Dash()
    {
        movementData.Dir.z = dashSpeed;
    }
    
    private void Update()
    {
        if (IsDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                IsDashing = false;
                animationController?.SetDashing(false);
            }
        }

        if (dashCooldownTimer > 0f) 
            dashCooldownTimer -= Time.deltaTime;
    }

    private void StartDash()
    {
        IsDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown + dashDuration;
        movementData.Dir.y = 0.01f;
        animationController?.SetDashing(true);
    }
}