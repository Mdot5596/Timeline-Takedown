using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    public int baseHealth = 500;
    public float baseSpeed = 2.5f;
    public int baseDamage = 50;

    private int health;
    private float speed;
    private int damage;

    public GameObject FinalBossReward; 
    private int waveNumber; 

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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
        health -= damageAmount;

        if (health <= 0)
        {
            Die();
        }
    }

public void Die()
{
    WaveManager waveManager = FindObjectOfType<WaveManager>();
    if (waveManager != null)
    {
        waveManager.EnemyDefeated();
    }

    // Ensure NavMeshAgent is valid before stopping
    if (agent != null && agent.isOnNavMesh)
    {
        agent.isStopped = true;
        agent.enabled = false; // Disable to prevent further movement
    }
    else
    {
        Debug.LogWarning("Boss NavMeshAgent is not on the NavMesh or missing.");
    }

    DropItem();
    Destroy(gameObject, 3f);
    GetComponent<EnemyController>().enabled = false;

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
} }

