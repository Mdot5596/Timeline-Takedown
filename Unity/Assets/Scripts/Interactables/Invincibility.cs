using UnityEngine;

public class InvincibilityPowerUp : MonoBehaviour
{
    public float duration = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthManager healthManager = other.GetComponentInParent<HealthManager>();
        //    PowerUpUI powerUpUI = FindObjectOfType<PowerUpUI>();

            if (healthManager != null)
            {
                healthManager.ActivateInvincibility(duration);
                Debug.Log("[Invincibility] Player is now invincible for " + duration + " seconds.");

                PowerUpUIManager uiManager = FindObjectOfType<PowerUpUIManager>();
if (uiManager != null)
{
    uiManager.ShowInvincibilityUI();
}

            }

            Destroy(gameObject);
        }
    }
}
