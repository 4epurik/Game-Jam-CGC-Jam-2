using System.Collections.Generic;
using Characteristics;
using TMPro;
using UnityEngine;

public class GameTimeController : SingletonBase<GameTimeController>
{
    private UtilityManager utility => UtilityManager.Instance;
    
    public float TotalTime { get; private set; }
    
    protected override void Awake()
    {
        base.Awake();
        Debug.Log("GameTimeController loaded");
    }
    
    public void SetTimeCounted(float time)
    {
        TotalTime = time;
    }

    public string GetTimeString()
    {
        return utility.TimeToText(TotalTime);
    }
    
 
}
