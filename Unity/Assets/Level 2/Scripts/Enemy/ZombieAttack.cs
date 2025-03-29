using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damage = 10; // Damage per hit
    public float attackRate = 1.5f; // Attack every X seconds

    private float nextAttackTime = 0f;
    private Animator anim; // Animator reference

    private void Start()
    {
        // Get the Animator component
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not found on Zombie!");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // If the attack cooldown has passed
            if (Time.time >= nextAttackTime)
            {
                // Play the attack animation once
                anim.SetTrigger("Attack");

                // Deal damage to the player
                HealthManager playerHealth = collision.gameObject.GetComponent<HealthManager>();
                DamageOverlay screenEffect = collision.gameObject.GetComponent<DamageOverlay>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    Debug.Log("Zombie dealt " + damage + " damage to player.");
                }

                if (screenEffect != null)
                {
                    screenEffect.ShowDamageEffect(); // Trigger red screen pulse
                }

                // Set next attack time
                nextAttackTime = Time.time + attackRate;
            }
        }
    }
}
