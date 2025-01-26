using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthbar;

    public int maxHealth = 100; // Maximum health
    private int currentHealth;

    private void Start()
    {
        // Get the HealthBar component attached to this enemy
        _healthbar = GetComponentInChildren<HealthBar>();

        if (_healthbar == null)
        {
            Debug.LogError("HealthBar component not found in children!");
            return;
        }
        
        currentHealth = maxHealth; // Initialise health
        _healthbar.UpdateHealthBar(maxHealth, currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
             _healthbar.UpdateHealthBar(maxHealth, currentHealth);
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // Destroy the enemy
    }
}