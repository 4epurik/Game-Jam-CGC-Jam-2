using UnityEngine;
using UnityEngine.UI;
using System;
using Script;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private CoinCollector _coinCollector;

    [Header("Settings")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    [Header("Audio Settings")]
    [SerializeField] private bool pauseMusicOnPause = true;

    private bool isPaused = false;
    private float gameTime = 0f;

    private void Start()
    {
        // Скрываем меню при старте
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
        // Обновляем игровое время только когда игра не на паузе
        if (!isPaused)
        {
            gameTime += Time.deltaTime;
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            UpdateUI();
        }
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // Обновление текста в UI
    private void UpdateUI()
    {
        coinsText.text = $":{_coinCollector.GetCoins().ToString()}";
    }

    // Метод для кнопки "Продолжить"
    public void ResumeGame()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }
}