using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private static GameTimer instance;
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
                    var singleton = new GameObject("Coin Collector");

                    instance = singleton.AddComponent<GameTimer>();
                }

                return instance;
            }

            return instance;
        }
    }

    void Update()
    {
        if (!isRunning) return;

        timeElapsed += Time.deltaTime; 
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60);
        int seconds = Mathf.FloorToInt(timeElapsed % 60);
        int milliseconds = Mathf.FloorToInt((timeElapsed * 100f) % 100f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
