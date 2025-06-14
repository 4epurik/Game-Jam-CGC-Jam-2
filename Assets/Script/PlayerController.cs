using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private int liveScore = 1;

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
        Time.timeScale = 1f; // ������� �����
        onGameStarted.Invoke();

        isGameStarted = true;
        Time.timeScale = 1f;

        if (menuUI != null)
            menuUI.SetActive(false); // �������� ����
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
}