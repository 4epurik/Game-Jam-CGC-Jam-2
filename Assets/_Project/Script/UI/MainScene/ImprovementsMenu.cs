using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Script.UI
{
    public class ImprovementsMenu : MonoBehaviour
    {
        [SerializeField] private Button backButton;
        public event Action OnBackClicked;
        
        private void Awake()
        {
            backButton.onClick.AddListener(()=>OnBackClicked?.Invoke() );
        }
    }
}