using UnityEngine;

public class Heal : MonoBehaviour, IInteractable
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
            healthManager.Heal(25); // Take 50 damage when interacted
            Debug.Log("Block Healed! Current health: " + healthManager.healthAmount);
        }
        else
        {
            Debug.LogWarning("No HealthManager attached to block!");
        }
    }
}
