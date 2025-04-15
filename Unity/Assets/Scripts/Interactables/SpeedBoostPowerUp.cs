using UnityEngine;

public class SpeedBoostPowerUp : MonoBehaviour
{
    public float boostedSpeed = 24f; // Double the normal speed
    public float duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement2 playerMovement = other.GetComponent<PlayerMovement2>();
            if (playerMovement == null)
            {
                playerMovement = other.GetComponentInChildren<PlayerMovement2>();
            }

            if (playerMovement != null)
            {
                playerMovement.ActivateSpeedBoost(boostedSpeed, duration);
                Debug.Log("Speed boost applied to player!");
                PowerUpUIManager uiManager = FindObjectOfType<PowerUpUIManager>();



if (uiManager != null)
{
    uiManager.ShowSpeedBoostUI();
}
            }

            Destroy(gameObject); // Remove power-up after use
        }
    }

    
}
