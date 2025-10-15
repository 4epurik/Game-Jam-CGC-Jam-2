using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class CoinCollector : SingletonBase<CoinCollector>
    {
        /*
        [SerializeField] private List<TextMeshProUGUI> textCountCoinsList;
        [SerializeField] private List<TextMeshProUGUI> textCountRecordCoinsList;
        */
        
        [SerializeField] private int coinsPerSpeedBoost = 10;
        [SerializeField] private int maxCoinAmountForBoost = 200;

        public event Action OnSpeedIncreased;
        public event Action OnNewCoinCollected;
        
        
        public int CoinAmount { get; private set; } = 0;

        protected override void Awake()
        {
            base.Awake();
            Debug.Log("CoinCollector loader");
        }

        private void Start()
        {
            //recordCoinCount = PlayerDataManager.Instance.LoadCoins();
        }
        
        /*public void UpdateText()
        {
            foreach (var textCount in textCountCoinsList)
            {
                SetText(CoinAmount, textCount);
            }

            if (CoinAmount > recordCoinCount)
                SetRecordCoin(CoinAmount);
        }
        
        public void SetRecordCoin(int coin)
        {
            foreach (var textCount in textCountRecordCoinsList)
                SetText(coin, textCount);
        }
        
        void SetText(int coin, TextMeshProUGUI text)
        {
            text.text = ":" + coin.ToString();
        }*/

        public void AddCoin()
        {
            CoinAmount++;
            OnNewCoinCollected?.Invoke();
            if (CoinAmount % coinsPerSpeedBoost == 0 && CoinAmount < maxCoinAmountForBoost )
                OnSpeedIncreased?.Invoke();
        }
    }
}