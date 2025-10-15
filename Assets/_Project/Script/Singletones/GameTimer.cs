using System;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    private float timeElapsed;

    private void Start()
    {
        LifeController.Instance.OnPlayerDeath += InstanceOnOnPlayerDeath;
    }

    private void InstanceOnOnPlayerDeath()
    {
        GameTimeController.Instance.SetTimeCounted(timeElapsed);
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        timerText.text = GameTimeController.TimeToText(timeElapsed);
    }

    private void OnDestroy()
    {
        if (LifeController.Instance != null)
            LifeController.Instance.OnPlayerDeath -= InstanceOnOnPlayerDeath;
    }
}