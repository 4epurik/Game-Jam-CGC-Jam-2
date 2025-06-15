using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private int liveScore = 1;
    [SerializeField] private GameObject backgroundMusic;

    [Header("Settings")]
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private float restartDelay = 3f;

    [Header("Events")]
    [SerializeField] private UnityEvent onGameStarted; // ������� ��� ������ ����
    [SerializeField] private UnityEvent onGameOver;   // ������� ��� ���������

    [Header("References")]
    [SerializeField] private GameObject gameOverUI;

    [Header("UI Settings")]
    [SerializeField] private GameObject menuUI; // ���������� ���� ���� �� �����
    [SerializeField] private GameObject timerObject;
    [SerializeField] private GameObject coinsObject;

    private bool isGameStarted = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        dir = Vector3.zero;
        Time.timeScale = 0f; // ���� ���������� �� �����
    }



    private void Update()
    {
        if (!isGameStarted) return; // ���������� ���������� �� ������

        if (Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Space))
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
    }

    // ���������� ������� Start � UI
    public void StartGame()
    {
        isGameStarted = true;
        backgroundMusic.SetActive(true);
        Time.timeScale = 1f; // ������� �����
        onGameStarted.Invoke();

        isGameStarted = true;
        Time.timeScale = 1f;

        SetUiActive();
    }

    private void SetUiActive()
    {
        if (menuUI != null)
            menuUI.SetActive(false); // �������� ����
        if (timerObject != null)
            timerObject.SetActive(true);
        if (coinsObject != null)
            coinsObject.SetActive(true);
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            dir.y = jumpForce;
        }
    }

    public void GameOver()
    {
        isGameStarted = false;
        gameOverUI.SetActive(true);
        onGameOver.Invoke();
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
        Debug.Log("Speed increased! New jumpForce: " + jumpForce);
    }
}