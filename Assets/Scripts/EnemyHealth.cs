using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public bool hostile = false;
    EnemyDrop enemyDrop;
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
        health -= takenAmount;
        if(health <= 0)
        {
            GameObject drop = enemyDrop.currentDrop;
            Instantiate(drop, transform.position, Quaternion.identity);
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
