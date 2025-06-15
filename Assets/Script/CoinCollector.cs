using TMPro;
using UnityEngine;

namespace Script
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCountCoins;
        [SerializeField] private PlayerController player;
        [SerializeField] private int coinsPerSpeedBoost = 10;
        [SerializeField] private int speedIncreaseAmount = 10;
        private  static CoinCollector instance;
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
            _textCountCoins.text = ": " + coinAmount.ToString();
        }

        public void AddCoin()
        {
            coinAmount++;
            UpdateText();
            if (coinAmount == coinsPerSpeedBoost )
            {
                player.IncreaseSpeed(speedIncreaseAmount);
            }
        }

        public int GetCoins()
        {
            return coinAmount;
        }
    }
}