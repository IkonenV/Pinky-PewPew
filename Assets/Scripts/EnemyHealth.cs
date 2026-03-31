using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public bool hostile = false;
    EnemyDrop enemyDrop;
    public AudioClip[] hurtSounds;
    public AudioClip[] deathSounds;
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
        SoundFXManager.instance.PlayRandomSoundFXClip(hurtSounds, transform, 1f);
        health -= takenAmount;
        if(health <= 0)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(deathSounds, transform, 0.2f);
            GameObject drop = enemyDrop.currentDrop;
            Instantiate(drop, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
            enemyDrop.NextOnList();
            Destroy(gameObject);
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
