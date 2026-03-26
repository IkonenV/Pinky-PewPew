using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyList;
    public int destroyedEnemies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyedEnemies >= enemyList.Length)
        {
            WaveSpawner waveSpawner = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>();
            waveSpawner.enabled = true;
        }
    }
public void TurnHostile()
{
    foreach (GameObject enemy in enemyList)
    {
        if (enemy != null) 
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            
            if (enemyHealth != null)
            {
                enemyHealth.hostile = true;
            }
        }
    }
}
}
