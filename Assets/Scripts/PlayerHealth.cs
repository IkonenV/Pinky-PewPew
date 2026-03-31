using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 100f;
    public float maxHealth = 100f;
    public Slider healthSlider;
    public GameObject gameOverObj;
    PlayerScore playerScore;
    public AudioClip[] takeDamageSounds;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        gameOverObj = GameObject.FindGameObjectWithTag("DeathMenu");
        gameOverObj.SetActive(false);
        Time.timeScale = 1;
        playerScore = GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(float takenAmount)
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(takeDamageSounds, transform, 0.3f);
        currentHealth -= takenAmount;
        healthSlider.value = currentHealth / 100;
        if(currentHealth <= 0)
        {
            GameOver();
        }
    }
    public void GetHealth(float gottenAmount)
    {
        currentHealth += gottenAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthSlider.value = currentHealth / 100;
        
    }
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverObj.SetActive(true);
        playerScore.GameOver();
    }
    public void TryAgain()
    {
        // 1. Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        // 2. Load it again using its name or build index
        SceneManager.LoadScene(currentScene.name);
    }
}
