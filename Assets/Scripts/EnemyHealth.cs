using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;
    public bool hostile = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
