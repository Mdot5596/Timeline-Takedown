using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private HealthBar _healthbar;

    private Animator anim;

    public int maxHealth = 100; // Maximum health
    private int currentHealth;
    private bool isDead = false; // Flag to prevent multiple calls to Die()

    private void Start()
    {

        // Get the Animator component
        anim = GetComponent<Animator>();

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

private void Die()
{
    if (isDead) return; // Prevent multiple death calls

    isDead = true; 
    Debug.Log("Enemy died!");

    // Check if this enemy has a BossAI script
    BossAI bossAI = GetComponent<BossAI>();
    if (bossAI != null)
    {
        bossAI.Die();  //  Calls the BossAI Die method to drop the item
    }

    FindObjectOfType<WaveManager>().EnemyDefeated(); 

        // Stop enemy movement
    if (GetComponent<UnityEngine.AI.NavMeshAgent>() != null)
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
    }

        // Play the death animation (if Animator is found)
        if (anim != null)
        {
            anim.SetTrigger("Die"); 
        }
        Destroy(gameObject, 3f); // Destroy after x seconds 
    }

}
