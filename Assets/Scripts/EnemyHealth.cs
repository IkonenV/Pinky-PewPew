using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public bool hostile = false;
    EnemyDrop enemyDrop;
    public AudioClip[] hurtSounds;
    public AudioClip[] deathSounds;
    public GameObject deathEffect;
    public Animator animator;
    public bool Dead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyDrop = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EnemyDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void TakeDamage(float takenAmount)
    {
        GameManager gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (!hostile)
        {
            gameManager.TurnHostile();
        }
        animator.SetTrigger("Hurt");
        SoundFXManager.instance.PlayRandomSoundFXClip(hurtSounds, transform, 1f);
        health -= takenAmount;
        if(health <= 0)
        {
            Dead = true;
            SoundFXManager.instance.PlayRandomSoundFXClip(deathSounds, transform, 0.2f);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            GameObject drop = enemyDrop.currentDrop;
            Instantiate(drop, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            enemyDrop.NextOnList();
            animator.SetTrigger("Death");
            Destroy(gameObject, 0.49f);

            gameManager.destroyedEnemies += 1;
        }
    }
      void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }
     
    }
}
