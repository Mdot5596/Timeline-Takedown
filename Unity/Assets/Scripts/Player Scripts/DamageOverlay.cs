using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageOverlay : MonoBehaviour
{
    public Image damageImage; // Assign in Inspector
    public float fadeDuration = 0.5f; // How fast it fades out
    private Color targetColor;
    private float fadeSpeed;

    private void Start()
    {
        targetColor = damageImage.color;
        targetColor.a = 0; // Start fully transparent
        damageImage.color = targetColor;
        fadeSpeed = 1f / fadeDuration;
    }

    public void ShowDamageEffect()
    {
        StopAllCoroutines();
        StartCoroutine(FadeEffect());
    }

    private IEnumerator FadeEffect()
    {
        // Flash red
        targetColor.a = 0.5f;
        damageImage.color = targetColor;

        // Fade out
        while (damageImage.color.a > 0)
        {
            targetColor.a -= fadeSpeed * Time.deltaTime;
            damageImage.color = targetColor;
            yield return null;
        }
    }
}
