using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20; // The amount of damage this bullet deals

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has an EnemyHealth component
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Apply damage to the enemy
            enemyHealth.TakeDamage(damage);
        }

        // Destroy the bullet upon collision
        Destroy(gameObject);
    }
}
