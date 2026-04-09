using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(Damage);

        }
        Destroy(gameObject);
    }
    void Start()
    {
        DestroyTime();
    }
    void DestroyTime()
    {
        Destroy(gameObject, 5f);
    }
}
