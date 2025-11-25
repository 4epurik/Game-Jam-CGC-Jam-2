using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

[Serializable]
public class MetaGameplayData : SingletonBase<MetaGameplayData>
{
    public string PlayerName => playerName;
    public int CurrentMoney => currentMoney;
    public float TotalTime => totalTime;

    public event Action OnCurrentMoneyChanged; 
    public event Action OnTotalTimeChanged; 
  
    [HideInInspector][SerializeField] private string playerName = "Default";
    [HideInInspector][SerializeField] private int currentMoney;
    [HideInInspector][SerializeField] private float totalTime;

    public bool TrySetPlayerName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;
        
        playerName = name;
        return true;
    }

    public bool TrySpendMoney(int money)
    {
        if (!IsMoneyEnough(money)) 
            return false;
        
        currentMoney -= money;
        OnCurrentMoneyChanged?.Invoke();
        return true;
    }

    public bool IsMoneyEnough(int money)
    {
        return currentMoney >= money;
    }

    public void AddMoney(int money)
    {
        currentMoney += money;
        OnCurrentMoneyChanged?.Invoke();
    }

    public void AddTotalTime(float totalTime)
    {
        this.totalTime += totalTime;
        OnTotalTimeChanged?.Invoke();
        
    }
    
}