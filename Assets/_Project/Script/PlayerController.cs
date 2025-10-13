using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [Header("Settings")]
    [SerializeField] private int initSpeed = 9;
    [SerializeField] private float initJumpForce = 9f;
    [SerializeField] private float baseGravity = -28f; // Убедись, что в Inspector отрицательное
    [SerializeField] private Animator anim;
    [SerializeField] private float dashSpeed = 20f; // Скорость рывка
    [SerializeField] private float dashDuration = 0.2f; // Длительность рывка
    [SerializeField] private float dashCooldown = 1f; // Кулдаун рывка
    [SerializeField] private int maxJumps = 2; // Максимум прыжков (2 для двойного)
    [SerializeField] private float gravityScale = 1f;
    
    private bool isGameStarted = false;
    private int speed;
    private float jumpForce;
    private int jumpCount; // Счётчик прыжков
    private bool isDashing; // Флаг рывка
    private float dashTimer; // Таймер рывка
    private float dashCooldownTimer; // Таймер кулдауна рывка
    private float gravity;
    
    void Start()
    {
        speed = initSpeed;
        jumpForce = initJumpForce;
        jumpCount = 0;
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

        // Прыжок по клику мышкой или пробелу
        if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) && jumpCount < maxJumps)
        {
            Jump();
        }

        // Рывок по кнопке (например, Shift или правая кнопка мыши)
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetMouseButtonDown(1))
        {
            if (dashCooldownTimer <= 0f && !isDashing)
            {
                StartDash();
            }
        }

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
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown + dashDuration; // Кулдаун начинается сразу
        if (anim != null)
            anim.SetBool("isDashing", true);
    }

    public void IncreaseSpeed(int amount)
    {
        speed += amount;
        UpdateGravity();
    }

    public void IncreaseJump(int amount)
    {
        jumpForce += amount;
    }

    public void SetSpeed(int newSpeed)
    {
        speed = newSpeed;
        UpdateGravity();
    }

    private void UpdateGravity()
    {
        gravity = baseGravity * (1f + gravityScale * (speed - initSpeed) / initSpeed);
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