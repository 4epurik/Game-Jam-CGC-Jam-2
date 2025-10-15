using UnityEngine;

public class PlayerDataManager : SingletonBase<PlayerDataManager>
{
    // Ключи для PlayerPrefs
    private const string TIME_KEY = "BestTime";
    private const string COINS_KEY = "TotalCoins";
    
    // Метод для сохранения времени (в секундах)
    public void SaveTime(float time)
    {
        PlayerPrefs.SetFloat(TIME_KEY, time);
        PlayerPrefs.Save();
    }

    // Метод для загрузки сохраненного времени
    public float LoadTime()
    {
        return PlayerPrefs.GetFloat(TIME_KEY, 0f);
    }

    // Метод для сохранения количества монет
    public void SaveCoins(int coins)
    {
        PlayerPrefs.SetInt(COINS_KEY, coins);
        PlayerPrefs.Save();
    }

    // Метод для загрузки количества монет
    public int LoadCoins()
    {
        return PlayerPrefs.GetInt(COINS_KEY, 0);
    }

    // Метод для добавления монет
    public void AddCoins(int amount)
    {
        int currentCoins = LoadCoins();
        currentCoins += amount;
        SaveCoins(currentCoins);
    }

    // Очистка сохраненных данных (для отладки или сброса)
    public void ClearData()
    {
        PlayerPrefs.DeleteKey(TIME_KEY);
        PlayerPrefs.DeleteKey(COINS_KEY);
        PlayerPrefs.Save();
        Debug.Log("Сохраненные данные очищены");
    }
}