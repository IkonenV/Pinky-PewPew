using UnityEngine;

public class Bullet : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(34);

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
