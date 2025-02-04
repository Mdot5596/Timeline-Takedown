using System.Collections;
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
    private float attackCooldown;
    
    // Base Stats (Modified by Waves)
    [SerializeField] private int baseHealth = 100;
    [SerializeField] private float baseSpeed = 3.5f;
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private float baseAttackCooldown = 1.5f;
    
    // Patrol
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    
    // Attack
    public GameObject projectile;
    public float sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    private bool alreadyAttacked;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void ScaleStats(int waveNumber)
    {
        // Increase stats based on wave number
        health = Mathf.RoundToInt(baseHealth * (1 + (waveNumber - 1) * 0.2f));
        speed = baseSpeed * (1 + (waveNumber - 1) * 0.05f);
        damage = Mathf.RoundToInt(baseDamage * (1 + (waveNumber - 1) * 0.15f));
        attackCooldown = baseAttackCooldown * (1 - (waveNumber - 1) * 0.05f);
        
        agent.speed = speed;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f) walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Enemy attack logic (Ranged)
            Rigidbody rb = Instantiate(projectile, transform.position + Vector3.up * 1.5f, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0) Die();
    }

public void Die()
{
    FindObjectOfType<WaveManager>().EnemyDefeated(); // Notify the WaveManager
    Destroy(gameObject);
}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
