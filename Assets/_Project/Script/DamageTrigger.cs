using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageTrigger : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private int damage = 1;
    private bool isHurt;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, вошёл ли объект с нужным тегом
        if (other.CompareTag(playerTag) && !isHurt)
        {
            isHurt = true;
            var screenFlach = other.GetComponent<ScreenFlash>();
            if (screenFlach != null)
                screenFlach.FlashEffectForDeath();
            LifeController.Instance.GetDamage(damage);
        }
    }
}