using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    private float currentHealth;
 public GameObject gameOverCanvas; // Reference to the Game Over Canvas


    void Start()
    {
        currentHealth = healthAmount;
        gameOverCanvas.SetActive(false); // Ensure the Game Over Canvas is disabled at start
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
      //      Application.LoadLevel(Application.LoadedLevel);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Heal(5);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
         currentHealth = healthAmount; // Sync currentHealth with healthAmount
        Debug.Log($"Player Health: {currentHealth}");
         if (currentHealth <= 0)
        {
            Die();
        }

    }

    public void Heal(float healingAmount)
{
    healthAmount += healingAmount;
    healthAmount = Mathf.Clamp(healthAmount,0, 100);

    healthBar.fillAmount = healthAmount / 100f;
}

  private void Die()
    {
        Debug.Log("Player Died!");
        gameOverCanvas.SetActive(true); 
        Time.timeScale = 0f; 
        // Add death logic here, such as reloading the level
    }

}