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

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    public void ScaleStats(int waveNumber)
    {
        health = baseHealth + (waveNumber * 100); // Boss gets stronger per wave
        speed = baseSpeed + (waveNumber * 0.2f);
        damage = baseDamage + (waveNumber * 10);

        agent.speed = speed;

//TEST LOG
        Debug.Log($"[BossAI] Boss Stats - Health: {health}, Speed: {speed}, Damage: {damage}");
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
        FindObjectOfType<WaveManager>().EnemyDefeated();
        Destroy(gameObject);
    }
}
