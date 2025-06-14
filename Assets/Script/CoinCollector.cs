using TMPro;
using UnityEngine;

namespace Script
{
    public class CoinCollector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCountCoins;
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
        }

        public int GetCoins()
        {
            return coinAmount;
        }
    }
}