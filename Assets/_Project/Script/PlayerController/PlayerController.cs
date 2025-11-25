using System;
using JetBrains.Annotations;
using Script;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementData
{
   public Vector3 Dir;
}

[RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CoinCollector coin => CoinCollector.Instance;
    private LifeController life => LifeController.Instance;
    private GameStateManager game => GameStateManager.Instance;
    
    [Header("Components")]
    [SerializeField] private AnimationController animationController;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerJumpAbility jumpAbility;
    [SerializeField] private PlayerDashAbility dashAbility;
    [SerializeField] private PlayerWaveAbility waveAbility;

    private CharacterController controller;
    private PlayerInput playerInput;
    private MovementData movementData;

    [UsedImplicitly] public void OnJump() => jumpAbility.TryJump();
    [UsedImplicitly] public void OnDash() => dashAbility.TryDash();
    [UsedImplicitly] public void OnPause() => game.SetPause(true);

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
        coin.OnSpeedIncreased += IncreaseSpeed;
        life.OnPlayerDeath += SetPlayerDead;
        
        animationController.Initialize();
        movement.StartPlayer();
    }
    
    private void FixedUpdate()
    {
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
    }
    
    private void OnEnable()
    {
        if (playerInput != null) playerInput.actions.Enable();
    }

    private void OnDisable()
    {
        if (playerInput != null) playerInput.actions.Disable();
    }
    
    private void IncreaseSpeed() => movement.IncreaseSpeed();
    private void SetPlayerDead()
    {
        movement.SetSpeed(0);
        animationController?.SetDead();
    }
}