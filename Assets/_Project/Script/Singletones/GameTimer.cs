using System;
using System.Collections.Generic;
using Characteristics;
using Script;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private GameTimeController time => GameTimeController.Instance;
    private UtilityManager utility => UtilityManager.Instance;
    private LifeController life => LifeController.Instance;
    
    [SerializeField] private TextMeshProUGUI timerText;
    
    private float timeElapsed;
    
    private void Start()
    {
        life.OnPlayerDeath += () => time.SetTimeCounted(timeElapsed);
    }
    
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        timerText.text = utility.TimeToText(timeElapsed);
    }
}