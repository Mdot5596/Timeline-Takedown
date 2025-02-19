using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthbar;

    public int maxHealth = 100; // Maximum health
    private int currentHealth;
    private bool isDead = false; // Flag to prevent multiple calls to Die()

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

    // Method to take damage and reduce health
    public void TakeDamage(int damage)
    {
        if (isDead) return; // If already dead, ignore further damage

        currentHealth -= damage; // Reduce health
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        // Update health bar after taking damage
        if (currentHealth > 0)
        {
            _healthbar.UpdateHealthBar(maxHealth, currentHealth);
        }
        else
        {
            Die();
        }
    }

    // Method to handle the death of the enemy
    private void Die()
    {
        if (isDead) return; // Prevent the death logic from being called more than once

        isDead = true; // Mark the enemy as dead
        Debug.Log("Enemy died!");

        // Notify WaveManager that this enemy has died
        FindObjectOfType<WaveManager>().EnemyDefeated();

        //  add a death animation or effect here

        Destroy(gameObject); // Destroy the enemy object
    }
}
