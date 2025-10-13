using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameExitTrigger : MonoBehaviour
{
    [Tooltip("Тег объекта, который должен активировать выход")]
    [SerializeField] private string playerTag = "Player";

    private bool isHurt;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, вошёл ли объект с нужным тегом
        if (other.CompareTag(playerTag) && !isHurt)
        {
            isHurt = true;
            LifeController.Instance.UpdateLife(true);
            var countLife = LifeController.Instance.GetLife();
            if (countLife <= 0)
            {
                LifeController.Instance.SetInitialLife();
                Debug.Log("Игрок вошёл в триггер. Выход из игры...");
                GameOver.Instance.SetGameOver();
            }
            else
            {
                var screenFlach = other.GetComponent<ScreenFlash>();
                if (screenFlach != null)
                    screenFlach.FlashEffectForDeath();
            }
        }
    }
}