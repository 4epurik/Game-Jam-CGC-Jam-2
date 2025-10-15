using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MetaGameplayData : SingletonBase<MetaGameplayData>
{
    public string PlayerName { get; private set; }
    public int CurrentMoney { get; private set; }
    public float TotalTime { get; private set; }

    public event Action OnCurrentMoneyChanged; 
    public event Action OnTotalTimeChanged; 

    public bool TrySetPlayerName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;
        PlayerName = name;
        return true;
    }

    public bool TrySpendMoney(int money)
    {
        if (!IsMoneyEnough(money)) 
            return false;
        
        CurrentMoney -= money;
        OnCurrentMoneyChanged?.Invoke();
        return true;
    }

    public bool IsMoneyEnough(int money)
    {
        return CurrentMoney >= money;
    }

    public void AddMoney(int money)
    {
        CurrentMoney += money;
        OnCurrentMoneyChanged?.Invoke();
    }

    public void AddTotalTime(float totalTime)
    {
        TotalTime += totalTime;
        OnTotalTimeChanged?.Invoke();
        
    }
    
}