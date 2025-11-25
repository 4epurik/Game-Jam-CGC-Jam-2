using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button toMainButton;

    private void OnEnable()
    {
        restartButton.onClick.AddListener(() => GameStateManager.Instance.RestartGame().Forget());
        continueButton.onClick.AddListener(() => GameStateManager.Instance.SetPause(false));
        toMainButton.onClick.AddListener(() => GameStateManager.Instance.ToMainMenu());
    }
}