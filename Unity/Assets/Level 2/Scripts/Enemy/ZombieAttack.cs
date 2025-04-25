using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public int damage = 10;
    public float attackRate = 1.5f; 

    private float nextAttackTime = 0f;
    private Animator anim; 

    private void Start()
    {
        // Get the Animator component
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator not found on Zombie!");
        }
    }

private void OnTriggerStay(Collider other)
{
    if (other.CompareTag("Player")) 
    {
        if (Time.time >= nextAttackTime)
        {
            anim.SetTrigger("Attack");

            HealthManager playerHealth = other.GetComponentInParent<HealthManager>();
            DamageOverlay screenEffect = other.GetComponentInParent<DamageOverlay>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            if (screenEffect != null)
            {
                screenEffect.ShowDamageEffect();
            }

            nextAttackTime = Time.time + attackRate;
        }
    }
}


}
