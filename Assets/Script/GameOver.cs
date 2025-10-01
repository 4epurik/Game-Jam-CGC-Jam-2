using System;
using UnityEngine;

namespace Script
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameOver gameOver;
        private static GameOver instance;
        
        public static GameOver Instance 
        {
            get
            {
                if(instance == null)
                {
                    if(GameObject.FindObjectOfType<GameOver>() == null)
                    {
                        var singleton = new GameObject("GameOver");
                        instance = singleton.AddComponent<GameOver>();
                    }
                    return instance;
                }
                return instance;
            }
        }

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            gameObject.SetActive(false);
        }

        public void Init()
        {
            
        }

        public void SetGameOver()
        {
            int currentCoins = CoinCollector.Instance.GetCoins();
            float currentTime = GameTimer.Instance.GetElapsedTime();
            int maxCoin = Mathf.Max(PlayerDataManager.Instance.LoadCoins(), currentCoins);
            float maxTime = Mathf.Max(PlayerDataManager.Instance.LoadTime(), currentTime);
            PlayerDataManager.Instance.SaveCoins(maxCoin);
            PlayerDataManager.Instance.SaveTime(maxTime);
            CoinCollector.Instance.UpdateText();
            CoinCollector.Instance.SetRecordCoin(maxCoin);
            GameTimer.Instance.UpdateTimerUI();
            GameTimer.Instance.SetRecordTime(maxTime);
            gameObject.SetActive(true);

            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }
}