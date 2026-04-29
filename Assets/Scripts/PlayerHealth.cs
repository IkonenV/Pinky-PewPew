using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth = 5f;
    public float maxHealth = 5f;
    public GameObject gameOverObj;
    PlayerScore playerScore;
    public AudioClip[] takeDamageSounds;
    public GameObject heart5;
    public GameObject heart4;
    public GameObject heart3;
    public GameObject heart2;
    public GameObject heart1;
    public Animator animator;
    public AudioClip[] buttonSounds;
    public AudioClip loseSound;
    public AudioSource wingSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverObj = GameObject.FindGameObjectWithTag("DeathMenu");
        gameOverObj.SetActive(false);
        Time.timeScale = 1;
        playerScore = GetComponent<PlayerScore>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth >= 5)
        {
            heart5.SetActive(true);
            heart4.SetActive(true);
            heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if(currentHealth == 4)
        {
            heart5.SetActive(false);
            heart4.SetActive(true);
            heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if(currentHealth == 3)
        {
            heart5.SetActive(false);
            heart4.SetActive(false);
            heart3.SetActive(true);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if(currentHealth == 2)
        {
            heart5.SetActive(false);
            heart4.SetActive(false);
            heart3.SetActive(false);
            heart2.SetActive(true);
            heart1.SetActive(true);
        }
        else if(currentHealth == 1)
        {
            heart5.SetActive(false);
            heart4.SetActive(false);
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(true);
        }
        else if(currentHealth <= 0)
        {
            heart5.SetActive(false);
            heart4.SetActive(false);
            heart3.SetActive(false);
            heart2.SetActive(false);
            heart1.SetActive(false);
        }
    }
    public void TakeDamage(float takenAmount)
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(takeDamageSounds, transform, 0.2f);
        currentHealth -= takenAmount;
        animator.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            GameOver();
        }
    }
    public void GetHealth(float gottenAmount)
    {
        currentHealth += gottenAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
    public void GameOver()
    {
        wingSource.Stop();
        SoundFXManager.instance.PlaySoundFXClip(loseSound, transform, 1f);
        Time.timeScale = 0;
        gameOverObj.SetActive(true);
        playerScore.GameOver();
    }
    public void TryAgain()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(buttonSounds, transform, 0.5f);
        StartCoroutine(LoadGameDelay());
    }
    public void LoadMenu()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(buttonSounds, transform, 0.5f);
        StartCoroutine(LoadMenuDelay());
    }
    IEnumerator LoadMenuDelay()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        SceneManager.LoadScene("Menu");
    }
        IEnumerator LoadGameDelay()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

}
