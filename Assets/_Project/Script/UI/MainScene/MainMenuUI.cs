using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button improvementsButton;
    [SerializeField] private Button quitButton;

    public event Action OnImprovementsClicked;
    
    private void Start()
    {
        startGameButton.onClick.AddListener(()=> GameStateManager.Instance.StartGame().Forget());
        quitButton.onClick.AddListener(Application.Quit);
        improvementsButton.onClick.AddListener(()=>OnImprovementsClicked?.Invoke());
    }
}
