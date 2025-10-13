using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private static GameTimer instance;
    public List<TextMeshProUGUI> timerTextList = new ();    
    public TextMeshProUGUI timerText;   
    private float timeElapsed;   
    private bool isRunning = true;

    public static GameTimer Instance 
    {
        get
        {
            if(instance == null)
            {
                if(GameObject.FindObjectOfType<GameTimer>() == null)
                {
                    var singleton = new GameObject("Game Timer");
                    instance = singleton.AddComponent<GameTimer>();
                }
                return instance;
            }
            return instance;
        }
    }
    
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    
    public void Init()
    {
            
    }

    void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime; 
        UpdateTimerUI();
    }

    public void UpdateTimerUI()
    {
        foreach (var timeText in timerTextList)
        {
            SetTimerValue(timeElapsed, timeText);
        }
    }
    
    public void SetRecordTime(float timeval)
    {
        SetTimerValue(timeval, timerText);
    }

    void SetTimerValue(float timeval, TextMeshProUGUI timeText)
    {
        int minutes = Mathf.FloorToInt(timeval / 60);
        int seconds = Mathf.FloorToInt(timeval % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetElapsedTime()
    {
        return timeElapsed;
    }
}
