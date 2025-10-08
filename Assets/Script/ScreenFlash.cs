using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.4f;
    public int flashCount = 1;

    public void FlashEffectForDeath()
    {
        StartCoroutine(FlashScreen());
    }

    private System.Collections.IEnumerator FlashScreen()
    {
        for (int i = 0; i < flashCount; i++)
        {
            // Показываем с полной прозрачностью
            flashImage.color = new Color(1, 0, 0, 0.5f);
            flashImage.CrossFadeAlpha(0.5f, flashDuration / 2, false);
            yield return new WaitForSeconds(flashDuration / 2);

            // Затухание
            flashImage.CrossFadeAlpha(0f, flashDuration / 2, false);
            yield return new WaitForSeconds(flashDuration / 2);
        }
    }
}