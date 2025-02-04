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

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
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

}
