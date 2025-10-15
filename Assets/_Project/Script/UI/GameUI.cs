using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeCount;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private TextMeshProUGUI coinsCount;

    private void Start()
    {
        UpdateLifeText();
        UpdateCoinsText();
        LifeController.Instance.OnLifeChanged += UpdateLifeText;
        CoinCollector.Instance.OnNewCoinCollected += UpdateCoinsText;
    }

    private void UpdateLifeText()
    {
        var countLifeText = LifeController.Instance.CurrentLife.ToString();
        lifeCount.text = ":" + countLifeText;
    }

    private void UpdateCoinsText()
    {
        coinsCount.text = ":" + CoinCollector.Instance.CoinAmount;
    }
}
