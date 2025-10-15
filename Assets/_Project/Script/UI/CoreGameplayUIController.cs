using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameplayUIController : MonoBehaviour
{
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private GameUI gameUI;
    [SerializeField] private PauseUI pauseUI;

    private enum UIWindows
    {
        GameOver,
        Game,
        Pause
    }

    private void Start()
    {
        GameStateManager.Instance.OnGameFinished += Instance_OnGameFinished;
        GameStateManager.Instance.OnPauseStateChanged += Instance_OnPauseStateChanged;
        SwitchToUI(UIWindows.Game);
    }

    private void Instance_OnPauseStateChanged(bool isPaused)
    {
        var window = isPaused ? UIWindows.Pause : UIWindows.Game;
        SwitchToUI(window);
    }

    private void Instance_OnGameFinished()
    {
        SwitchToUI(UIWindows.GameOver);
    }

    private void OnDestroy()
    {
        if (GameStateManager.Instance != null)
        {
            GameStateManager.Instance.OnGameFinished -= Instance_OnGameFinished;
            GameStateManager.Instance.OnPauseStateChanged -= Instance_OnPauseStateChanged;
        }
    }

    private void SwitchToUI(UIWindows window)
    {
        gameOverUI.gameObject.SetActive(window == UIWindows.GameOver);
        gameUI.gameObject.SetActive(window == UIWindows.Game);
        pauseUI.gameObject.SetActive(window == UIWindows.Pause);
    }
}