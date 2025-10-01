using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Script
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> textCountCoinsList;
        [SerializeField] private TextMeshProUGUI textCountCoins;
        [SerializeField] private PlayerController player;
        [SerializeField] private int coinsPerSpeedBoost = 10;
        [SerializeField] private int speedIncreaseAmount = 10;
        [SerializeField] private int jumpIncreaseAmount = 3;
        private static CoinCollector instance;
        private int coinAmount = 0;

        private void Start()
        {
            UpdateText();
        }

        public static CoinCollector Instance 
        {
            get
            {
                if(instance == null)
                {
                    if(GameObject.FindObjectOfType<CoinCollector>() == null)
                    {
                        var singleton = new GameObject("Coin Collector");

                        instance = singleton.AddComponent<CoinCollector>();
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
        public void UpdateText()
        {
            foreach (var textCount in textCountCoinsList)
            {
                SetText(coinAmount, textCount);
            }
        }
        
        public void SetRecordCoin(int coin)
        {
            SetText(coin, textCountCoins);
        }
        

        void SetText(int coin, TextMeshProUGUI text)
        {
            text.text = ": " + coin.ToString();
        }

        public void AddCoin()
        {
            coinAmount++;
            UpdateText();
            if (coinAmount == coinsPerSpeedBoost )
            {
                player.IncreaseSpeed(speedIncreaseAmount);
                player.IncreaseJump(jumpIncreaseAmount);
            }
        }

        public int GetCoins()
        {
            return coinAmount;
        }
    }
}