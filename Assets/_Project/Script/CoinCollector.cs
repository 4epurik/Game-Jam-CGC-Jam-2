using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> textCountCoinsList;
        [SerializeField] private List<TextMeshProUGUI> textCountRecordCoinsList;
        
        //[SerializeField] private PlayerController player;
        [SerializeField] private int coinsPerSpeedBoost = 10;
        [SerializeField] private int speedIncreaseAmount = 10;
        [SerializeField] private int jumpIncreaseAmount = 3;
        [SerializeField] private int maxCoinAmountForBoost = 200;

        public event Action<int> OnSpeedIncreased;
        
        private static CoinCollector instance;
        private int coinAmount = 0;
        private int recordCoinCount;

        private void Start()
        {
            recordCoinCount = PlayerDataManager.Instance.LoadCoins();
            UpdateText();
            SetRecordCoin(recordCoinCount);
        }

        public static CoinCollector Instance 
        {
            get
            {
                if (instance != null) 
                    return instance;
                
                if(GameObject.FindObjectOfType<CoinCollector>() == null)
                {
                    var singleton = new GameObject("Coin Collector");

                    instance = singleton.AddComponent<CoinCollector>();
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
        
        public void UpdateText()
        {
            foreach (var textCount in textCountCoinsList)
            {
                SetText(coinAmount, textCount);
            }

            if (coinAmount > recordCoinCount)
                SetRecordCoin(coinAmount);
        }
        
        public void SetRecordCoin(int coin)
        {
            foreach (var textCount in textCountRecordCoinsList)
                SetText(coin, textCount);
        }
        

        void SetText(int coin, TextMeshProUGUI text)
        {
            text.text = ":" + coin.ToString();
        }

        public void AddCoin()
        {
            coinAmount++;
            UpdateText();
            if (coinAmount % coinsPerSpeedBoost == 0 && coinAmount < maxCoinAmountForBoost )
            {
                OnSpeedIncreased?.Invoke(speedIncreaseAmount);
                //player.IncreaseSpeed(speedIncreaseAmount);
                //player.IncreaseJump(jumpIncreaseAmount);
            }
        }

        public int GetCoins()
        {
            return coinAmount;
        }
    }
}