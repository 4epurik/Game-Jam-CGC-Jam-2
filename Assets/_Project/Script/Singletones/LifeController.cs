using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Script
{
    public class LifeController : SingletonBase<LifeController>
    {
        public event Action OnPlayerDeath;
        public event Action OnLifeChanged;
        
        private const int initialLife = 3;
        
        public int CurrentLife { get; private set; }

        private void Start()
        {
            SetInitialLife();
        }
        
        public void GetDamage(int damage = 1)
        {
            CurrentLife = math.max(0, CurrentLife-damage);
            OnLifeChanged?.Invoke();
            if (CurrentLife == 0)
                OnPlayerDeath?.Invoke();
                
        }
        
        public void SetInitialLife()
        {
            CurrentLife = initialLife;
            OnLifeChanged?.Invoke();
        }
    }
}