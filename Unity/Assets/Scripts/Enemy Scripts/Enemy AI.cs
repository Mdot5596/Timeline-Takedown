using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    
    // Stats
    private int health;
    private float speed;
    private int damage;
    
    // Base Stats (Modified by Waves)
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private float baseSpeed = 3.5f;
    [SerializeField] private int baseDamage = 10;

    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootForce = 20f;
    public float shootCooldown = 1.5f;
    private bool readyToShoot = true;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (player != null)
        {
            agent.SetDestination(player.position);

            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < 15f) // Set shooting range
            {
                ShootAtPlayer();
            }
        }
    }

    public void ScaleStats(int waveNumber)
    {
        health = Mathf.RoundToInt(baseHealth * (1 + (waveNumber - 1) * 0.2f));
        speed = baseSpeed * (1 + (waveNumber - 1) * 0.05f);
        damage = Mathf.RoundToInt(baseDamage * (1 + (waveNumber - 1) * 0.15f));

        agent.speed = speed;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<WaveManager>().EnemyDefeated();
        Destroy(gameObject);
    }

private void ShootAtPlayer()
{
    if (readyToShoot && bulletPrefab != null && shootPoint != null)
    {
        readyToShoot = false;

        // Instantiate the bullet at the shoot point
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Use shootPoint.forward to apply force in its forward direction
            Vector3 force = shootPoint.forward * shootForce;
            rb.AddForce(force, ForceMode.Impulse);
        }

        Invoke(nameof(ResetShot), shootCooldown);
    }
}



    private void ResetShot()
    {
        readyToShoot = true;
    }
}
