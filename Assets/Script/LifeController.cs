using TMPro;
using UnityEngine;

namespace Script
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCountLife;
        private const int initialLife = 3;
        private static LifeController instance;
        private static int countLife = initialLife;

        private void Start()
        {
            UpdateText();
        }

        public static LifeController Instance 
        {
            get
            {
                if(instance == null)
                {
                    if(GameObject.FindObjectOfType<LifeController>() == null)
                    {
                        var singleton = new GameObject("Life");

                        instance = singleton.AddComponent<LifeController>();
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
            var countLifeText = countLife.ToString();
            if (countLife < 0)
                countLifeText = "0";
            _textCountLife.text = ": " + countLifeText;
        }

        public void UpdateLife(bool reduceLife = false)
        {
            if (reduceLife)
                countLife--;
            else
                countLife++;
            UpdateText();
        }
        
        public void SetInitialLife()
        {
            countLife = initialLife;
            UpdateText();
        }

        public int GetLife()
        {
            return countLife;
        }
    }
}