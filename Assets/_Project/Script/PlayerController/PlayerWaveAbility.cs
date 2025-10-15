using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWaveAbility : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private float waveForce = 3f;
    [SerializeField] private float waveDistance = .5f;
    [SerializeField] private float waveCooldown = 1f;
    [SerializeField] private float waveActiveTime = 0.5f;
    [SerializeField] private LayerMask waveLayerMask;
    [SerializeField] private float waveActivationThreshold = 0.8f;


    private InputAction waveDirectionAction;
    private Vector2 lastWaveDirection;
    private MovementData movementData;
    private float waveTimer;
    private float waveCooldownTimer;

    
    public Vector3 WaveImpulse { get; private set; } = Vector3.zero;
    

    public void Initialize(PlayerInput playerInput, MovementData movementData)
    {
        this.movementData = movementData;
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput is required for PlayerWaveAbility.");
            return;
        }

        waveDirectionAction = playerInput.actions["WaveDirection"];
        if (waveDirectionAction == null)
        {
            Debug.LogError("WaveDirection action not found in PlayerInputActions!");
        }
    }

    private void Update()
    {
        // Управление таймерами
        if (waveCooldownTimer > 0f) waveCooldownTimer -= Time.deltaTime;

        if (waveTimer > 0f)
        {
            waveTimer -= Time.deltaTime;
            if (waveTimer <= 0f)
            {
                // Сброс импульса, когда время действия волны истекло
                WaveImpulse = Vector3.zero;
            }
        }

        // Проверка активации волны
        if (waveCooldownTimer <= 0f && waveDirectionAction != null)
        {
            Vector2 inputDir = waveDirectionAction.ReadValue<Vector2>();
            
            // Логика активации по порогу или ненулевому вводу (как в исходном коде)
            bool shouldActivate = (inputDir.magnitude > waveActivationThreshold) || 
                                  (inputDir != Vector2.zero && waveDirectionAction.activeControl?.device is Keyboard);

            if (shouldActivate)
            {
                lastWaveDirection = inputDir.normalized;
                TryWave(lastWaveDirection);
                waveCooldownTimer = waveCooldown;
            }
        }
    }

    private void TryWave(Vector2 inputDir)
    {
        Vector3 direction = new Vector3(0f, inputDir.y, inputDir.x).normalized;

        RaycastHit hit;
        Debug.DrawRay(transform.position, direction * waveDistance, Color.blue, 1f);

        if (Physics.Raycast(transform.position, direction, out hit, waveDistance, waveLayerMask))
        {
            Vector3 oppositeDirection = -direction.normalized;
            Vector3 calculatedImpulse = oppositeDirection * waveForce;
            
            WaveImpulse = calculatedImpulse; // Сохраняем импульс
            waveTimer = waveActiveTime;      // Запускаем таймер активности
            
            Debug.Log($"Wave activated! Hit: {hit.collider.name}");
            movementData.Dir.y = waveForce;
        }
    }
}