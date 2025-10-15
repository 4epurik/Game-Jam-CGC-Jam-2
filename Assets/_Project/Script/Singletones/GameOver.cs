using System.Collections;
using UnityEngine;

namespace Script
{
    public class GameOver : SingletonBase<GameOver>
    {
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private float delayForDeath = 2f;
        
        protected override void Awake()
        {
            base.Awake();
            gameOverCanvas.SetActive(false);
        }

        public void SetGameOver()
        {
            //GameStateManager.Instance.EndGame();
            //playerController.SetPlayerDead();
            StartCoroutine(ShowDeathScreenAfterAnimation());
        }
        public void Die()
        {
            StartCoroutine(ShowDeathScreenAfterAnimation());
        }

        private IEnumerator ShowDeathScreenAfterAnimation()
        {
            // Ждём окончания анимации
            yield return new WaitForSeconds(delayForDeath);

            // Показываем экран смерти
            //int currentCoins = CoinCollector.Instance.GetCoins();
            //float currentTime = GameTimer.Instance.GetElapsedTime();
           // int maxCoin = Mathf.Max(PlayerDataManager.Instance.LoadCoins(), currentCoins);
           // float maxTime = Mathf.Max(PlayerDataManager.Instance.LoadTime(), currentTime);
          //  PlayerDataManager.Instance.SaveCoins(maxCoin);
          //  PlayerDataManager.Instance.SaveTime(maxTime);
          //  CoinCollector.Instance.UpdateText();
          //  CoinCollector.Instance.SetRecordCoin(maxCoin);
           // GameTimer.Instance.UpdateTimerUI();
           // GameTimer.Instance.SetRecordTime(maxTime);
            gameOverCanvas.SetActive(true);

            Time.timeScale = 0f;
            Cursor.visible = true;
        }
    }
}