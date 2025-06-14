using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameUI; // UI �������� ����������

    public void StartGame()
    {
        menuUI.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1f; // ������� �����, ���� ����
    }
}