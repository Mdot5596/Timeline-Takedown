using UnityEngine;


//Make sure to remove all debug logs once in levels
//Also maybe try fix- thinks health manager is on playerobj not player?idk
public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { Heal, InstantKill }
    public PowerUpType powerUpType;

    public float rotationSpeed = 50f;

    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
{
    Debug.Log($"[PowerUp] Touched: {other.gameObject.name}");

    if (other.CompareTag("Player"))
    {
        ApplyEffect(other.gameObject);
        Destroy(gameObject);
    }
    else
    {
        Debug.LogWarning($"[PowerUp] Ignored: {other.gameObject.name} (Tag: {other.tag})");
    }
}

private void ApplyEffect(GameObject detectedObject)
{
    Debug.Log($"[PowerUp] Checking {detectedObject.name} for HealthManager...");

    // Try getting HealthManager directly
    HealthManager healthManager = detectedObject.GetComponent<HealthManager>();

    // If not found, check parent objects
    if (healthManager == null)
    {
        Debug.LogWarning($"[PowerUp] {detectedObject.name} has NO HealthManager! Checking parent...");
        healthManager = detectedObject.GetComponentInParent<HealthManager>();
    }

    if (healthManager != null)
    {
        healthManager.Heal(50);
        Debug.Log("[PowerUp] Player healed! New health: " + healthManager.healthAmount);
    }
    else
    {
        Debug.LogError("[PowerUp] ERROR: HealthManager STILL NOT FOUND, even in parent!");
    }
}


}

