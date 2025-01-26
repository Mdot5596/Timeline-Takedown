using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth; // Initialise health
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health
        Debug.Log($"Enemy took {damage} damage. Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject); // Destroy the enemy
    }
}