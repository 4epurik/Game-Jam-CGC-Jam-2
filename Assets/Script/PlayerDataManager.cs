using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    // Ключи для PlayerPrefs
    private const string TIME_KEY = "BestTime";
    private const string COINS_KEY = "TotalCoins";
    private static PlayerDataManager instance;
    
    public static PlayerDataManager Instance 
    {
        get
        {
            if(instance == null)
            {
                if(GameObject.FindObjectOfType<PlayerDataManager>() == null)
                {
                    var singleton = new GameObject("PlayerDataManager");
                    instance = singleton.AddComponent<PlayerDataManager>();
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
    
    // Метод для сохранения времени (в секундах)
    public void SaveTime(float time)
    {
        PlayerPrefs.SetFloat(TIME_KEY, time);
        PlayerPrefs.Save();
        Debug.Log($"Сохранено время: {time} секунд");
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
        Debug.Log($"Сохранено монет: {coins}");
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