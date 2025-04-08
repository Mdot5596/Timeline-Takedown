using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f; 
    public float bulletLifetime = 5f; 
    public float shootForce = 20f;
    public float upwardForce = 0f;

    private Rigidbody rb;

private void Start()
{
    rb = GetComponent<Rigidbody>();

    Collider bulletCollider = GetComponent<Collider>();
    
    // Ignore collisions with all enemy colliders (including Boss)
    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
    foreach (GameObject enemy in enemies)
    {
        foreach (Collider enemyCol in enemy.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(bulletCollider, enemyCol);
        }
    }

    // Optional: ignore boss too if it's not tagged as "Enemy"
    GameObject boss = GameObject.FindGameObjectWithTag("Boss");
    if (boss != null)
    {
        foreach (Collider bossCol in boss.GetComponentsInChildren<Collider>())
        {
            Physics.IgnoreCollision(bulletCollider, bossCol);
        }
    }

    if (rb == null)
    {
        Debug.LogError("Rigidbody not attached to the bullet!");
        return;
    }

    Vector3 forceDirection = transform.forward * shootForce + transform.up * upwardForce;
    rb.AddForce(forceDirection, ForceMode.Impulse);

    Destroy(gameObject, bulletLifetime);
}


private void OnCollisionEnter(Collision collision)
{
    Debug.Log($"Bullet hit: {collision.gameObject.name}");

    // Check if the object hit is the Player before applying damage
    if (collision.gameObject.CompareTag("Player"))
    {
        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();

        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);
        }
    }

    // Destroy bullet on impact (regardless of what it hits)
    Destroy(gameObject);
}

}
