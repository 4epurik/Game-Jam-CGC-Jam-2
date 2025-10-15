using System;
using JetBrains.Annotations;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementData
{
   public Vector3 Dir;
}

// Добавим RequireComponent для обеспечения наличия всех нужных компонентов
[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    
    [SerializeField] private AnimationController animationController;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerJumpAbility jumpAbility;
    [SerializeField] private PlayerDashAbility dashAbility;
    [SerializeField] private PlayerWaveAbility waveAbility;

    private CharacterController controller;
    private PlayerInput playerInput;
    private MovementData movementData;

    [UsedImplicitly]
    public void OnJump() => jumpAbility.TryJump();

    [UsedImplicitly]
    public void OnDash() => dashAbility.TryDash();
    
    public void IncreaseSpeed(int amount) => movement.IncreaseSpeed(amount);
    public void SetSpeed(int newSpeed) => movement.SetSpeed(newSpeed);
    public void StartPlayer() => movement.StartPlayer();

    public void SetPlayerDead()
    {
        movement.SetSpeed(0);
        animationController?.SetDead();
    }
    
    private void Awake()
    {
        movementData = new MovementData();
        playerInput = GetComponent<PlayerInput>();
        controller = GetComponent<CharacterController>();

        movement.Initialize(movementData, animationController);
        jumpAbility.Initialize(movementData, animationController);
        dashAbility.Initialize(movementData, animationController);
        waveAbility.Initialize(playerInput, movementData);
    }

    private void Start()
    {
        CoinCollector.Instance.OnSpeedIncreased += IncreaseSpeed;
        GameStarter.Instance.OnGameStarted += StartPlayer;
        
        animationController.Initialize();
    }
    
    private void FixedUpdate()
    {
        if (!GameStateManager.Instance.IsGameStarted) return;
        if (dashAbility.IsDashing)
            dashAbility.Dash();
        else
        {
            movement.Move();
            movement.ApplyGravity(Time.fixedDeltaTime);
        }
        controller.Move(movementData.Dir * Time.fixedDeltaTime);
        
        if (controller.isGrounded)
            jumpAbility.ResetJumps();
        
        /*if (!GameStateManager.Instance.IsGameStarted) return;

        dir.z = isDashing ? dashSpeed : speed;
        if (!isDashing) 
            dir.y += baseGravity * Time.fixedDeltaTime;
        
        controller.Move(dir * Time.fixedDeltaTime);

        if (controller.isGrounded)
        {
            jumpCount = 0;
            if (animationController != null && !isDashing) animationController.SetJumping(false);
        }*/
    }
    
  

    private void OnEnable()
    {
        if (playerInput != null) playerInput.actions.Enable();
    }

    private void OnDisable()
    {
        if (playerInput != null) playerInput.actions.Disable();
    }

    private void OnDestroy()
    {
        if (CoinCollector.Instance != null)
            CoinCollector.Instance.OnSpeedIncreased -= IncreaseSpeed;
        if (GameStarter.Instance != null)
            GameStarter.Instance.OnGameStarted -= StartPlayer;
    }
}