using UnityEngine;
using UnityEngine.InputSystem; // Нужен для Initialize

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float baseGravity = -28f;
    [SerializeField] private int initSpeed = 9;
    
    private int speed;
    private AnimationController animationController;
    private MovementData movementData;
    
    public void Initialize(MovementData movementData, AnimationController anim)
    {
        this.speed = initSpeed;
        this.animationController = anim;
        this.movementData = movementData;
    }

    public void Move()
    {
        movementData.Dir.z = speed;
    }

    public void ApplyGravity(float deltaTime)
    {
        movementData.Dir.y += baseGravity * deltaTime; 
    }

    public void StartPlayer()
    {
        animationController?.SetRunning(true);
    }
    
    public void IncreaseSpeed(int amount) { speed += amount; }
    public void SetSpeed(int newSpeed) { speed = newSpeed; }
}