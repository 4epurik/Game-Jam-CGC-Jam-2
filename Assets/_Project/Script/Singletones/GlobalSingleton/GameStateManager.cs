using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Script;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/*
public enum GameStates
{
    MainMenu,
    GameStarted,
    GameInProcess,
    GamePaused,
    GameFinished
}
*/

public class GameStateManager : SingletonBase<GameStateManager>
{
    public event Action OnGameStarted;
    public event Action OnGameFinished;
    public event Action<bool> OnPauseStateChanged;

    [SerializeField] private float delayForDeath = 2f;

    private void Start()
    {
        //если стартуем с RogueScene StartGame не вызовется
        if (SceneManager.GetActiveScene().name == "RogueScene")
        {
            LifeController.Instance.OnPlayerDeath += EndGame;
            OnGameStarted?.Invoke();
        }
    }

    public async UniTask StartGame()
    {
        await LoadGameScene();
        LifeController.Instance.OnPlayerDeath += EndGame;
        Time.timeScale = 1.0f;
        OnGameStarted?.Invoke();
    }

    private async UniTask LoadGameScene()
    {
        await SceneManager.LoadSceneAsync("RogueScene");
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
        LifeController.Instance.OnPlayerDeath -= EndGame;
        SceneManager.LoadScene("MainMenu");
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
        int currentCoins = CoinCollector.Instance.CoinAmount;
        float currentTime = GameTimeController.Instance.TotalTime;
        //int maxCoin = Mathf.Max(PlayerDataManager.Instance.LoadCoins(), currentCoins);
        //float maxTime = Mathf.Max(PlayerDataManager.Instance.LoadTime(), currentTime);
        MetaGameplayData.Instance.AddMoney(currentCoins);
        MetaGameplayData.Instance.AddTotalTime(currentTime);
        SaveManager.Instance.SaveAll();
    }
    
    public async UniTask RestartGame()
    {
       await SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
       Time.timeScale = 1.0f;
       OnGameStarted?.Invoke();
    }
    
    
    /*public GameStates State { get; private set; }
    public event Action OnGameStateChanged;
    
    private bool isStartClicked;
    private bool isQuitClicked;
    private bool isPauseClicked;
    private bool isStartingFinished;
    
    public void StartGame()
    {
        isStartClicked = true;
        ChangeCurrentState();
    }

    public void StartingFinished()
    {
        isStartingFinished = true;
        ChangeCurrentState();
    }
    
    public void QuitGame()
    {
        isQuitClicked = true;
        ChangeCurrentState();
    }

    public void PauseGame()
    {
        isPauseClicked = true;
        ChangeCurrentState();
    }
    public void EndGame()
    {
        IsGameStarted = false;
    }

    private void ChangeCurrentState()
    {
        switch(State)
        {
            case GameStates.MainMenu:
                if (isStartClicked)
                {
                    State = GameStates.GameStarted;
                    isStartClicked = false;
                }

                if (isQuitClicked)
                {
                    isQuitClicked = false;
                    Application.Quit();
                }
                break;
            case GameStates.GameStarted:
                if (isStartingFinished)
                {
                    isStartingFinished = false;
                    GameStates.GameStarted
                }
                break;
            case GameStates.GameInProcess:
                if (isPauseClicked)
                {
                    isPauseClicked = false;
                    State = GameStates.GamePaused;
                }
                break;
            case GameStates.GamePaused:
                
                break;
            case GameStates.GameFinished:
                break;
        }
    }*/
}