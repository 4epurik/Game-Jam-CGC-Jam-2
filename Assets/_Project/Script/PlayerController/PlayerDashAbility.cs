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
        if (!GameStateManager.Instance.IsGameStarted) return;
        
        // Обновление таймеров рывка
        if (IsDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                IsDashing = false;
                // Анимация
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
        // Кулдаун начинается сразу после активации, плюс длительность рывка
        dashCooldownTimer = dashCooldown + dashDuration;
        movementData.Dir.y = 0.01f;   
        // Анимация
        animationController?.SetDashing(true);
    }
}