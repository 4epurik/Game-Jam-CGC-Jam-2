using JetBrains.Annotations;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int initSpeed = 9;
    [SerializeField] private float initJumpForce = 9f;
    [SerializeField] private float baseGravity = -28f;
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private AnimationController animationController;

    [Header("Wave Settings")]
    [SerializeField] private float waveForce = 3f; // Увеличил для заметности, настрой в Inspector
    [SerializeField] private float waveDistance = .5f;
    [SerializeField] private float waveCooldown = 1f;
    [SerializeField] private float waveActiveTime = 0.5f;
    [SerializeField] private LayerMask waveLayerMask;
    [SerializeField] private float waveActivationThreshold = 0.8f; // Порог для стика

    private CharacterController controller;
    private Vector3 dir;
    private int speed;
    private float jumpForce;
    private int jumpCount;
    private bool isDashing;
    private float dashTimer;
    private float dashCooldownTimer;
    private float waveTimer;
    private float waveCooldownTimer;

    // Input System через компонент
    private PlayerInput playerInput;
    private InputAction waveDirectionAction; // Ссылка на действие WaveDirection
    private Vector2 lastWaveDirection; // Для хранения направления после активации

    // Для импульса волны (persistent)
    private Vector3 waveImpulse;

    void Awake()
    {
        CoinCollector.Instance.OnSpeedIncreased += IncreaseSpeed;
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component not found on this GameObject!");
            return;
        }

        // Получаем действие по имени (убедись, что имя точно "WaveDirection" в .asset)
        waveDirectionAction = playerInput.actions["WaveDirection"];
        if (waveDirectionAction == null)
        {
            Debug.LogError("WaveDirection action not found in PlayerInputActions!");
        }
    }

    void Start()
    {
        speed = initSpeed;
        jumpForce = initJumpForce;
        jumpCount = 0;
        controller = GetComponent<CharacterController>();
        dir = Vector3.zero;
        isDashing = false;
        dashTimer = 0f;
        dashCooldownTimer = 0f;
        waveTimer = 0f;
        waveCooldownTimer = 0f;
        waveImpulse = Vector3.zero;
        animationController?.Initialize();
    }

    private void Update()
    {
        if (!GameStateManager.Instance.IsGameStarted) return;

        // Обновление таймеров
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
                animationController?.SetDashing(false);
            }
        }

        if (dashCooldownTimer > 0f) dashCooldownTimer -= Time.deltaTime;
        if (waveTimer > 0f) waveTimer -= Time.deltaTime;
        if (waveCooldownTimer > 0f) waveCooldownTimer -= Time.deltaTime;

        // Проверка активации волны (только если действие существует)
        if (waveCooldownTimer <= 0f && waveDirectionAction != null)
        {
            Vector2 inputDir = waveDirectionAction.ReadValue<Vector2>();

            // Для стика: активация по порогу, для клавиатуры: по ненулевому вводу
            bool shouldActivate = (inputDir.magnitude > waveActivationThreshold) || 
                                  (inputDir != Vector2.zero && waveDirectionAction.activeControl?.device is Keyboard);

            if (shouldActivate)
            {
                lastWaveDirection = inputDir.normalized;
                TryWave(lastWaveDirection);
                waveCooldownTimer = waveCooldown; // Кулдаун после активации
            }
        }
    }

    void FixedUpdate()
    {
        if (!GameStateManager.Instance.IsGameStarted) return;

        dir.z = isDashing ? dashSpeed : speed;
        if (!isDashing) dir.y += baseGravity * Time.fixedDeltaTime;
        
        controller.Move(dir * Time.fixedDeltaTime);

        if (controller.isGrounded)
        {
            jumpCount = 0;
            if (animationController != null && !isDashing) animationController.SetJumping(false);
        }
    }

    [UsedImplicitly]
    public void OnJump()
    {
        if (!GameStateManager.Instance.IsGameStarted) return;
        if (jumpCount < maxJumps) Jump();
    }

    [UsedImplicitly]
    public void OnDash()
    {
        if (!GameStateManager.Instance.IsGameStarted) return;
        if (dashCooldownTimer <= 0f && !isDashing) StartDash();
    }

    public void StartPlayer()
    {
        animationController?.SetRunning(true);
    }

    private void Jump()
    {
        dir.y = jumpForce;
        jumpCount++;
        animationController?.SetJumping(true);
    }

    private void StartDash()
    {
        dir.y = 0.1f;
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown + dashDuration;
        animationController?.SetDashing(true);
    }

    private void TryWave(Vector2 inputDir)
    {
        if (inputDir == Vector2.zero)
            return;
        
        Vector3 direction = new Vector3(0f, inputDir.y, inputDir.x).normalized;

        RaycastHit hit;
        Debug.DrawRay(transform.position, direction * waveDistance, Color.blue, 1f);
        if (Physics.Raycast(transform.position, direction, out hit, waveDistance, waveLayerMask))
        {
            Vector3 oppositeDirection = -direction.normalized;
            // Применяем импульс к waveImpulse (для persistence)
            waveImpulse = oppositeDirection * waveForce;
            dir.y = waveImpulse.y;
            waveTimer = waveActiveTime;

            Debug.Log($"Wave activated! Direction: {direction}, Hit: {hit.collider.name}, Impulse: {waveImpulse}");
        }
    }

    public void IncreaseSpeed(int amount) { speed += amount; }
    public void IncreaseJump(int amount) { jumpForce += amount; }
    public void SetSpeed(int newSpeed) { speed = newSpeed; }
    public void SetPlayerDead()
    {
        SetSpeed(0);
        animationController?.SetDead();
    }

    void OnEnable()
    {
        if (playerInput != null) playerInput.actions.Enable();
    }

    void OnDisable()
    {
        if (playerInput != null) playerInput.actions.Disable();
    }
}