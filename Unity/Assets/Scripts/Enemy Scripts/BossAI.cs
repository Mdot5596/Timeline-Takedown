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

    public GameObject dropItem; 
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

        Debug.Log($"[BossAI] Wave {waveNumber} Boss - Health: {health}, Speed: {speed}, Damage: {damage}");
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
        Debug.Log($"[BossAI] Boss Defeated!");

        if (waveNumber == 5 && dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
            Debug.Log("[BossAI] Wave 10 Boss Dropped Reward!");
        }

        FindObjectOfType<WaveManager>().EnemyDefeated();
        Destroy(gameObject);
    }
}
