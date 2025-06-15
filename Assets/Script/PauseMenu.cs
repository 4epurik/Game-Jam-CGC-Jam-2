using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text timeText;
    [SerializeField] private Button resumeButton;

    [Header("Settings")]
    [SerializeField] private KeyCode pauseKey = KeyCode.Escape;

    private bool isPaused = false;
    private float gameTime = 0f;
    private int coinsCollected = 0;

    private void Start()
    {
        // Настраиваем кнопку "Продолжить"
        resumeButton.onClick.AddListener(TogglePause);

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

    // Метод для сбора монет (вызывайте его при подборе монеты)
    public void AddCoin()
    {
        coinsCollected++;
        UpdateUI();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        // Ставим/снимаем паузу
        Time.timeScale = isPaused ? 0f : 1f;

        // Показываем/скрываем меню
        pauseMenu.SetActive(isPaused);

        // Обновляем UI при открытии меню
        if (isPaused)
        {
            UpdateUI();
        }

        // Управление курсором
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    // Обновление текста в UI
    private void UpdateUI()
    {
        // Форматируем время в минуты:секунды
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        string timeString = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

        coinsText.text = $"Монеты: {coinsCollected}";
        timeText.text = $"Время: {timeString}";
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