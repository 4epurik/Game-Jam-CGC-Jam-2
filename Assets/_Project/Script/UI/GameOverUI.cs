using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button FinishButton;
    [SerializeField] private TextMeshProUGUI coinsCount;
    [SerializeField] private TextMeshProUGUI coinsRecord;
    [SerializeField] private TextMeshProUGUI timeRecord;
    [SerializeField] private TextMeshProUGUI timeCurrent;
    private void OnEnable()
    {
        Debug.Log("GameOverUI loaded");
        SetText(CoinCollector.Instance.CoinAmount, coinsCount);
        timeCurrent.text = GameTimeController.Instance.GetTimeString();
        FinishButton.onClick.AddListener( GameStateManager.Instance.ToMainMenu);
    }

    private void SetText(int coin, TextMeshProUGUI text)
    {
        text.text = ":" + coin;
    }
}