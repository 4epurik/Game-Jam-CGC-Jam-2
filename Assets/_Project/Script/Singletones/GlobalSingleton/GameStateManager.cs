using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Script;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameStateManager : SingletonBase<GameStateManager>
{
    private LifeController life => LifeController.Instance;
    private CoinCollector coin => CoinCollector.Instance;
    private GameTimeController gameTime => GameTimeController.Instance;
    private MetaGameplayData meta => MetaGameplayData.Instance;
    private SaveManager save => SaveManager.Instance;
    
  
    [SerializeField] private float delayForDeath = 2f;
 
    public event Action OnGameStarted;
    public event Action OnGameFinished;
    public event Action<bool> OnPauseStateChanged;

    private void Start()
    {
        //если стартуем с RogueScene StartGame не вызовется, дебаг онли
        if (SceneManager.GetActiveScene().name == "RogueScene")
        {
            life.OnPlayerDeath += EndGame;
            OnGameStarted?.Invoke();
        }
    }

    /// <summary>
    /// Начало новой игры по кнопке главного меню
    /// </summary>
    public async UniTask StartGame()
    {
        await LoadGameScene();
        life.OnPlayerDeath += EndGame;
        Time.timeScale = 1.0f;
        OnGameStarted?.Invoke();
    }
    
    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0.0f : 1.0f;
        Cursor.visible = pause;
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        OnPauseStateChanged?.Invoke(pause);
    }

    public void ToMainMenu()
    {
        life.OnPlayerDeath -= EndGame;
        SceneManager.LoadScene("MainMenu");
    }
    
    public async UniTask RestartGame()
    {
        await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
        OnGameStarted?.Invoke();
    }
    
    private async UniTask LoadGameScene()
    {
        await SceneManager.LoadSceneAsync("RogueScene");
    }
    
    private void EndGame()
    {
        ShowGameEndScreen().Forget();
    }

    private async UniTask ShowGameEndScreen()
    {
        await UniTask.WaitForSeconds(0.1f);
        AddNewValues();
        await UniTask.WaitForSeconds(delayForDeath - 0.1f);
        OnGameFinished?.Invoke();
    }

    private void AddNewValues()
    {
        int currentCoins = coin.CoinAmount;
        float currentTime = gameTime.TotalTime;
        //int maxCoin = Mathf.Max(PlayerDataManager.Instance.LoadCoins(), currentCoins);
        //float maxTime = Mathf.Max(PlayerDataManager.Instance.LoadTime(), currentTime);
        meta.AddMoney(currentCoins);
        meta.AddTotalTime(currentTime);
        save.SaveAll();
    }
}