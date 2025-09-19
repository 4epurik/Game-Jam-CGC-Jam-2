using Script;
using TMPro;
using UnityEngine;

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
                
                            // Останавливаем игру (в редакторе)
                #if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
                #else
                            // Закрываем приложение в билде
                            Application.Quit();
                #endif
            }

        }
    }
}