using UnityEngine;
using UnityEngine.UI;
using System;
using Script;
using TMPro;
using UnityEngine.Audio; 

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Button resumeButton;
    [SerializeField] private CoinCollector _coinCollector;

    [Header("Settings")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    [Header("Audio Settings")]
   
    [SerializeField] private bool pauseMusicOnPause = true;

    private bool isPaused = false;
    private float gameTime = 0f;

    private void Start()
    {
        // Настраиваем кнопку "Продолжить"
        //resumeButton.onClick.AddListener(TogglePause);

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
        // Форматируем время в минуты:секунды
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        string timeString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        coinsText.text = $":{_coinCollector.GetCoins().ToString()}";
        // timeText.text = $"Время: {timeString}";
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