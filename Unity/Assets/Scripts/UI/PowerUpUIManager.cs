using System.Collections;
using UnityEngine;

public class PowerUpUIManager : MonoBehaviour
{
    public GameObject invincibilityUI;
    public GameObject healUI;
    public GameObject instantKillUI;
    public GameObject speedboostUI;

    public float displayDuration = 3f;

    public void ShowInvincibilityUI()
    {
        StartCoroutine(ShowUI(invincibilityUI));
    }

    public void ShowHealUI()
    {
        StartCoroutine(ShowUI(healUI));
    }

    public void ShowInstantKillUI()
    {
        StartCoroutine(ShowUI(instantKillUI));
    }

    public void ShowSpeedBoostUI()
    {
    StartCoroutine(ShowUI(speedboostUI));
    }


    private IEnumerator ShowUI(GameObject uiElement)
    {
        if (uiElement == null) yield break;

        uiElement.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        uiElement.SetActive(false);
    }
}
