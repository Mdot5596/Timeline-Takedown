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
    public bool IsDead => isDead;


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

    BossAI bossAI = GetComponent<BossAI>();
    if (bossAI != null)
    {
        bossAI.Die();
    }

    EnemyAI enemyAI = GetComponent<EnemyAI>();
if (enemyAI != null)
{
    enemyAI.enabled = false;
}


    FindObjectOfType<WaveManager>()?.EnemyDefeated(); 

    // Check if NavMeshAgent is valid, enabled, and on the NavMesh
    var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    if (agent != null && agent.enabled && agent.isOnNavMesh)
    {
        agent.isStopped = true; // Safely stop the agent
        agent.enabled = false;  // Disable it to prevent further errors
    }

    // Play death animation if available
    if (anim != null)
    {
        anim.SetTrigger("Die"); 
    }

    Destroy(gameObject, 3f); // Destroy after x seconds
}

}
