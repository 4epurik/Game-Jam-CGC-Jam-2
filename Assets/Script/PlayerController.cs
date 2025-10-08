using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [Header("Settings")]
    [SerializeField] private int initSpeed = 9;
    [SerializeField] private float initJumpForce = 9f;
    [SerializeField] private float gravity; 
    [SerializeField] private Animator anim;
    
    private bool isGameStarted = false;
    private int speed;
    private float jumpForce;
    void Start()
    {
        speed = initSpeed;
        jumpForce = initJumpForce;
        controller = GetComponent<CharacterController>();
        dir = Vector3.zero;
        if (anim != null)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isDead", false);
        }
        dir = Vector3.zero;
    }
    private void Update()
    {
        if (!isGameStarted) return;

        // Прыжок по клику мышкой или пробелу
        if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) && controller.isGrounded)
            Jump();
    }

    void FixedUpdate()
    {
        if (!isGameStarted) return;

        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);

        // если персонаж на земле → выключаем прыжок
        if (controller.isGrounded && anim != null)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isRunning", true);
        }
    }

    // Вызывается кнопкой Start в UI
    public void StartPlayer()
    {
        isGameStarted = true;
        // включаем анимацию бега
        if (anim != null)
            anim.SetBool("isRunning", true);
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            dir.y = jumpForce;
            if (anim != null)
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isJumping", true); // включаем анимацию прыжка
            }
        }
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
        anim.SetBool("isRunning", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isDead", true);
    }
}
