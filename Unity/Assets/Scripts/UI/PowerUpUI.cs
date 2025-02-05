using UnityEngine;
using TMPro;
using System.Collections;

public class PowerUpUI : MonoBehaviour
{
    public TextMeshProUGUI powerUpText;
    public float displayTime = 2f; // How long the text is visible

    private void Start()
    {
        powerUpText.gameObject.SetActive(false); 
    }

    public void ShowPowerUpMessage(string message)
    {
        StopAllCoroutines(); 
        StartCoroutine(DisplayMessage(message));
    }

//How tf can  i display diff images
    private IEnumerator DisplayMessage(string message)
    {
        powerUpText.text = message;
    //    powerUpImage.sprite = icon;
     //   powerUpPanel.SetActive(true);
        powerUpText.gameObject.SetActive(true); // Show message
        yield return new WaitForSeconds(displayTime);
        powerUpText.gameObject.SetActive(false); // Hide message
    }
}
