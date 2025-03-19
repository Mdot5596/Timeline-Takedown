using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damage = 10; // Damage per hit
    public float attackRate = 1.5f; // Attack every X seconds

    private float nextAttackTime = 0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
                DamageOverlay screenEffect = collision.gameObject.GetComponent<DamageOverlay>(); // Get the red pulse effect

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    Debug.Log("Zombie dealt " + damage + " damage to player.");
                }

                if (screenEffect != null)
                {
                    screenEffect.ShowDamageEffect(); // Trigger red screen pulse
                }

                nextAttackTime = Time.time + attackRate;
            }
        }
    }
}
