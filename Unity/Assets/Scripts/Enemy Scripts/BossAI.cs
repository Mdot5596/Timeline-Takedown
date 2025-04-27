using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;

    public int baseHealth = 500;
    public float baseSpeed = 2.5f;
    public int baseDamage = 50;

    private int health;
    private float speed;
    private int damage;

    public GameObject FinalBossReward;
    private int waveNumber;

    private bool isDying = false;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); 
    }

    public void ScaleStats(int wave)
    {
        waveNumber = wave;
        health = baseHealth + (wave * 100);
        speed = baseSpeed + (wave * 0.2f);
        damage = baseDamage + (wave * 10);

        agent.speed = speed;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDying) return; 
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDying) return; // Already dying, prevent double call
        isDying = true;

        // Stop agent
        if (agent != null && agent.isOnNavMesh)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        else
        {
            Debug.LogWarning("Boss NavMeshAgent is not on the NavMesh or missing.");
        }

        // Disable EnemyController immediately
        GetComponent<EnemyController>().enabled = false;

        
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
        else
        {
            Debug.LogWarning("Animator not found on Boss!");
        }

       
        DropItem();

        
        Destroy(gameObject, 3f);
    }

    void DropItem()
    {
        if (FinalBossReward != null)
        {
            Instantiate(FinalBossReward, transform.position + Vector3.up * 1f, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("FinalBossReward prefab is not assigned in the Inspector!");
        }
    }
}
