using UnityEngine;

public class Damage : MonoBehaviour, IInteractable
{
    public HealthManager healthManager; 

    private void Start()
    {
        if (healthManager == null)
        {
            healthManager = GetComponent<HealthManager>();
        }
    }

    public void Interact()
    {
        if (healthManager != null)
        {
            healthManager.TakeDamage(50); // Take 50 damage when interacted
            Debug.Log("Block gave damage! Current health: " + healthManager.healthAmount);
        }
        else
        {
            Debug.LogWarning("No HealthManager attached to block!");
        }
    }
}
