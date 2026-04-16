using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public ParticleSystem particleSystem1;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(Damage);

        }
        DetachAll();  
        Destroy(gameObject, 0.01f);
    }
    void Start()
    {
        DestroyTime();
    }
    void DestroyTime()
    {
        Destroy(gameObject, 5f);
    }
public void DetachAll()
{
    // Etsii kaikki hiukkassysteemit tästä objektista ja sen lapsista
    ParticleSystem[] allSystems = GetComponentsInChildren<ParticleSystem>();

    foreach (var ps in allSystems)
    {
        ps.transform.parent = null; // Irrottaa jokaisen
        var main = ps.main;
        main.stopAction = ParticleSystemStopAction.Destroy;
        ps.Stop();
    }
}
}
