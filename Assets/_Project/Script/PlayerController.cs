using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [Header("Settings")] [SerializeField] private int initSpeed = 9;
    [SerializeField] private float initJumpForce = 9f;
    [SerializeField] private float baseGravity = -28f; // Убедись, что в Inspector отрицательное
    [SerializeField] private Animator anim;
    [SerializeField] private float dashSpeed = 20f; // Скорость рывка
    [SerializeField] private float dashDuration = 0.2f; // Длительность рывка
    [SerializeField] private float dashCooldown = 1f; // Кулдаун рывка
    [SerializeField] private int maxJumps = 2; // Максимум прыжков (2 для двойного)

    private bool isGameStarted = false;
    private int speed;
    private float jumpForce;
    private int jumpCount; // Счётчик прыжков
    private bool isDashing; // Флаг рывка
    private float dashTimer; // Таймер рывка
    private float dashCooldownTimer; // Таймер кулдауна рывка
 

    void Start()
    {
        speed = initSpeed;
        jumpForce = initJumpForce;
        jumpCount = 0;
        SetSpeed(initSpeed);
        controller = GetComponent<CharacterController>();
        dir = Vector3.zero;
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isDead", false);
            anim.SetBool("isDashing", false); // Добавь bool в Animator для рывка
        }
    }

    private void Update()
    {
        if (!isGameStarted) return;
        
        // Обновление таймеров
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                if (anim != null)
                    anim.SetBool("isDashing", false);
            }
        }

        if (dashCooldownTimer > 0f)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// from input System
    /// </summary>
    [UsedImplicitly]
    public void OnJump()
    {
        if (jumpCount < maxJumps)
            Jump();
    }
    
    [UsedImplicitly]
    public void OnDash()
    {
        if (dashCooldownTimer <= 0f && !isDashing)
        {
            StartDash();
        }
    }

    void FixedUpdate()
    {
        if (!isGameStarted) return;

        // Движение вперёд: базовая скорость или рывок
        dir.z = isDashing ? dashSpeed : speed;
        if (!isDashing)
            dir.y += baseGravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);

        // Если на земле — сброс прыжков и анимации
        if (controller.isGrounded)
        {
            jumpCount = 0;
            if (anim != null && !isDashing)
            {
                anim.SetBool("isJumping", false);
                anim.SetBool("isRunning", true);
            }
        }
    }

    // Вызывается кнопкой Start в UI
    public void StartPlayer()
    {
        isGameStarted = true;
        if (anim != null)
            anim.SetBool("isRunning", true);
    }

    private void Jump()
    {
        dir.y = jumpForce;
        jumpCount++;
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", true);
        }
    }

    private void StartDash()
    {
        dir.y = .1f;
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown + dashDuration; // Кулдаун начинается сразу
        if (anim != null)
            anim.SetBool("isDashing", true);
    }

    public void IncreaseSpeed(int amount)
    {
        speed += amount;
    }

    public void IncreaseJump(int amount)
    {
        jumpForce += amount;
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
    }
    
    public void SetPlayerDead()
    {
        isGameStarted = false;
        SetSpeed(0);
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isDashing", false);
            anim.SetBool("isDead", true);
        }
    }
}