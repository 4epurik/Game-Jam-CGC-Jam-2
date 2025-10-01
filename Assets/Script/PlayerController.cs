using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private int liveScore = 1;
    [SerializeField] private GameObject backgroundMusic;

    private Animator anim; // добавлено: ссылка на Animator

    [Header("Settings")]
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float restartDelay = 3f;

    [Header("Events")]
    [SerializeField] private UnityEvent onGameStarted; // Событие при старте игры
    [SerializeField] private UnityEvent onGameOver;   // Событие при проигрыше

    [Header("References")]
    [SerializeField] private GameObject gameOverUI;

    [Header("UI Settings")]
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject timerObject;
    [SerializeField] private GameObject coinsObject;
    [SerializeField] private GameObject lifeObject;

    private bool isGameStarted = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>(); // добавлено: ищем аниматор на персонаже

        dir = Vector3.zero;
        Time.timeScale = 0f; // Игра начинается на паузе

        if (anim != null)
        {
            anim.SetBool("isGameStarted", false);
            anim.SetBool("isJumping", false);
            anim.SetBool("isDead", false);
        }
    }

    private void Update()
    {
        if (!isGameStarted) return;

        // Прыжок по клику мышкой или пробелу
        if ((Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space)) && controller.isGrounded)
        {
            Jump();
        }
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
        }
    }

    // Вызывается кнопкой Start в UI
    public void StartGame()
    {
        isGameStarted = true;
        backgroundMusic.SetActive(true);
        Time.timeScale = 1f;
        onGameStarted.Invoke();

        SetUiActive();

        // включаем анимацию бега
        if (anim != null)
        {
            anim.SetBool("isGameStarted", true);
        }
    }

    private void SetUiActive()
    {
        if (menuUI != null)
            menuUI.SetActive(false);
        if (timerObject != null)
            timerObject.SetActive(true);
        if (coinsObject != null)
            coinsObject.SetActive(true);
        if (lifeObject != null)
            lifeObject.SetActive(true);
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            dir.y = jumpForce;
            if (anim != null)
            {
                anim.SetBool("isJumping", true); // включаем анимацию прыжка
            }
        }
    }

    public void GameOver()
    {
        isGameStarted = false;
        gameOverUI.SetActive(true);
        onGameOver.Invoke();

        if (anim != null)
        {
            anim.SetBool("isDead", true); // включаем анимацию смерти
        }

        Invoke(nameof(RestartGame), restartDelay);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseSpeed(int amount)
    {
        speed += amount;
        Debug.Log("Speed increased! New speed: " + speed);
    }

    public void IncreaseJump(int amount)
    {
        jumpForce += amount;
        Debug.Log("Jump increased! New jumpForce: " + jumpForce);
    }
}
