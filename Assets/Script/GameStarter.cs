using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI; // UI �������� ����������

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f; // ������� �����, ���� ����
    }
}