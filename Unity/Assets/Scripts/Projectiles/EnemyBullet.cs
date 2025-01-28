using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f; 
    public float bulletLifetime = 5f; 
    public float shootForce = 20f;
    public float upwardForce = 0f;


    private Rigidbody rb; // Reference to the Rigidbody component



    private void Start()
    {
        // Get the Rigidbody component attached to the bullet
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not attached to the bullet!");
            return;
        }

        // Apply initial force to the bullet
        Vector3 forceDirection = transform.forward * shootForce + transform.up * upwardForce;
        rb.AddForce(forceDirection, ForceMode.Impulse);

    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Bullet hit: {collision.gameObject.name}");

        HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();

        if (healthManager != null)
        {
            healthManager.TakeDamage(damage);

            Destroy(gameObject);
        }
        
      //  else
      //  {
      //     Destroy(gameObject, 12f); // Destroy after 1 2  sexc
      //  }
    }
}