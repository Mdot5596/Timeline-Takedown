using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;
    public float bulletLifetime = 5f;
    public float shootForce = 20f;
    public float upwardForce = 0f;

    public Rigidbody rb; // Now public, so we can assign it manually if needed

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        if (rb == null)
            Debug.LogError("Rigidbody missing on bullet (Awake)!", gameObject);
    }

    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody still missing on bullet (Start)!", gameObject);
                return;
            }
        }

        Debug.Log("Bullet Rigidbody found and force applied", gameObject);

        Vector3 forceDirection = transform.forward * shootForce + transform.up * upwardForce;
        rb.AddForce(forceDirection, ForceMode.Impulse);

        Destroy(gameObject, bulletLifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HealthManager health = collision.gameObject.GetComponent<HealthManager>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
