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

private void ApplyEffect(GameObject player)
{
    HealthManager healthManager = player.GetComponentInParent<HealthManager>();
    EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
    PowerUpUI powerUpUI = FindObjectOfType<PowerUpUI>(); // Find UI

    if (powerUpType == PowerUpType.Heal)
    {
        if (healthManager != null)
        {
            healthManager.Heal(50);
            Debug.Log("[PowerUp] Player healed! New health: " + healthManager.healthAmount);
          PowerUpUIManager uiManager = FindObjectOfType<PowerUpUIManager>();
           if (uiManager != null)
              {
                uiManager.ShowHealUI(); // Or ShowInstantKillUI()
              }

        }
    }
    else if (powerUpType == PowerUpType.InstantKill)
    {
        if (enemySpawner != null)
        {
            enemySpawner.InstantKillAllEnemies();
            Debug.Log("[PowerUp] All enemies instantly killed!");

            // Show UI message
            if (powerUpUI != null)
            {
                powerUpUI.ShowPowerUpMessage("Instant Kill Activated!"); //Need to think of power up names

            }
        }
    }
}


}

