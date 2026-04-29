using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public ParticleSystem particleSystem1;
    public GameObject deathEffect;
    public AudioClip deathSound;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(Damage);

        }
        else
        {
            SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, 0.25f);
        }
        DetachParticle(); 
        DetachParticle2();
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
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
        Transform[] allGameobjects = gameObject.GetComponentsInChildren<Transform>();

        foreach (var ps in allGameobjects)
        {
            ps.transform.parent = null; // Irrottaa jokaisen
            ParticleSystem system1 = ps.GetComponent<ParticleSystem>();
            system1.Stop();
        }
    }
    public void DetachParticle()
    {
     GameObject particle = gameObject.transform.GetChild(0).gameObject;   
     particle.GetComponent<ParticleSystem>().Stop();
     Destroy(particle, 2f);
     particle.transform.parent = null;
     particle.transform.localScale = new Vector3(5,5,5);

    }

    public void DetachParticle2()
    {
     GameObject particle = gameObject.transform.GetChild(0).gameObject;   
     particle.GetComponent<ParticleSystem>().Stop();
     Destroy(particle, 2f);
     particle.transform.parent = null;
     particle.transform.localScale = new Vector3(1,1,1);

    }

}
