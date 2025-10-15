using System;
using Script;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public sealed class GameStarter : SingletonBase<GameStarter>
{
    [Header("Settings")]
    [SerializeField] private float restartDelay = 3f;
    
    [Header("References")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject backgroundMusic;

    [Header("UI Settings")]
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject coinsObject;
    [SerializeField] private GameObject lifeObject;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private ReloadGame reloadGame;
    [SerializeField] private GameObject recordObject;
    
    public event Action OnGameStarted; // Событие при старте игры
    public event Action OnGameOver;   // Событие при проигрыше
    
    private bool isGameStarted = false;

    void Start()
    {
        Time.timeScale = 0f;
        if (reloadGame.needReloadGame)
        {
            reloadGame.needReloadGame = false;
            StartGame();
        }
    }

    // Вызывается кнопкой Start в UI
    public void StartGame()
    {
        isGameStarted = true;
        backgroundMusic.SetActive(true);
        Time.timeScale = 1f;
        OnGameStarted?.Invoke();
        SetUiActive();
        GameStateManager.Instance.StartGame();
    }

    private void SetUiActive()
    {
        if (menuUI != null)
            menuUI.SetActive(false);
        if (gameOver != null)
            gameOver.SetActive(false);
        if (recordObject != null)
            recordObject.SetActive(true);
        if (coinsObject != null)
            coinsObject.SetActive(true);
        if (lifeObject != null)
            lifeObject.SetActive(true);
    }

    public void GameOverPlayer()
    {
        isGameStarted = false;
        gameOverUI.SetActive(true);
        OnGameOver.Invoke();

        Invoke(nameof(RestartGame), restartDelay);
    }

    public void RestartGame()
    {
        reloadGame.needReloadGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}