using UnityEngine;

public class SpiderBullet : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(34);

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
