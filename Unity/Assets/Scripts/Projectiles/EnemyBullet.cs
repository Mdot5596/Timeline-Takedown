using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f; // Amount of damage the bullet deals

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Bullet hit: {collision.gameObject.name}");
        // Check if the object hit has a PlayerHealth script (assuming the player has this script)
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();

        if (healthManager != null)
        {
            // Reduce the player's health
            healthManager.TakeDamage(damage);

            // Destroy the bullet after it hits the player
         //   Destroy(gameObject);
        }
        else
        {
            // Optional: Destroy the bullet if it hits something else
         //  Destroy(gameObject, 2f); // Destroy after 2 seconds if needed
        }
    }
}