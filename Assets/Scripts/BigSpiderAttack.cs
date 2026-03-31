using UnityEngine;

public class BigSpiderAttack : MonoBehaviour
{
    public float damage = 10f;
    public float knockbackForce = 5f;
    private bool canDealDamage = false;
    public AudioClip bigBiteSound;


    public void StartAttack()
    {
        canDealDamage = true;
    }
    public void EndAttack() => canDealDamage = false;
    public void StartAttackSound() => SoundFXManager.instance.PlaySoundFXClip(bigBiteSound, transform, 1f);

    private void OnTriggerStay(Collider other)
    {
        if (canDealDamage && other.CompareTag("Player"))
        {
            // 1. Deal Damage
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            // 2. Apply Knockback
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Calculate direction: from this weapon/enemy towards the player
                Vector3 moveDirection = other.transform.position - transform.position;
                moveDirection.y = 0; // Keep the knockback horizontal
                moveDirection.Normalize();

                // Apply the force
                rb.AddForce(moveDirection * knockbackForce, ForceMode.Impulse);
            }

            // Prevent hitting multiple times in one swing
            canDealDamage = false; 
        }
    }
}
