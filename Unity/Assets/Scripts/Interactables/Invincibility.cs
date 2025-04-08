using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviour
{
    public float duration = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager healthManager = other.GetComponentInParent<HealthManager>();
            PowerUpUI powerUpUI = FindObjectOfType<PowerUpUI>();

            if (healthManager != null)
            {
                healthManager.ActivateInvincibility(duration);
                Debug.Log("[Invincibility] Player is now invincible for " + duration + " seconds.");

                if (powerUpUI != null)
                {
                    powerUpUI.ShowPowerUpMessage("Invincible for " + duration + " seconds!");
                }
            }

            Destroy(gameObject);
        }
    }
}
