using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button toMainButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(() => GameStateManager.Instance.RestartGame());
        continueButton.onClick.AddListener(() => GameStateManager.Instance.SetPause(false));
        toMainButton.onClick.AddListener(() => GameStateManager.Instance.ToMainMenu());
    }
}