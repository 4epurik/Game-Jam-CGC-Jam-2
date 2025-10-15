using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimeController : SingletonBase<GameTimeController>
{
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameTimeController loaded");
    }

    public float TotalTime { get; private set; }

    public void SetTimeCounted(float time)
    {
        TotalTime = time;
    }

    public string GetTimeString()
    {
        return TimeToText(TotalTime);
    }
    
    public static string TimeToText(float timeval)
    {
        int minutes = Mathf.FloorToInt(timeval / 60);
        int seconds = Mathf.FloorToInt(timeval % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
