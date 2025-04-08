using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    public Image healthBar;
    public float healthAmount = 100f;

    [Header("Game Over UI")]
    public GameObject gameOverCanvas;

    [Header("Invincibility")]
    public bool isInvincible = false;
    public float invincibilityTimer = 0f;

    private void Start()
    {
        if (healthBar == null)
        {
            Debug.LogWarning("Health bar not assigned in inspector.");
        }

        gameOverCanvas.SetActive(false);
        healthBar.fillAmount = healthAmount / 100f;
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                Debug.Log("Invincibility ended.");
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible)
        {
            Debug.Log("Player is invincible. No damage taken.");
            return;
        }

        healthAmount -= damage;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;

        Debug.Log("Took damage: " + damage);
        Debug.Log("Current health: " + healthAmount);

        if (healthAmount <= 0)
        {
            Die();
        }
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
        healthBar.fillAmount = healthAmount / 100f;

        Debug.Log("Healed: " + healingAmount + " | Current health: " + healthAmount);
    }

    public void ActivateInvincibility(float duration)
    {
        isInvincible = true;
        invincibilityTimer = duration;
        Debug.Log("Invincibility activated for " + duration + " seconds.");
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
