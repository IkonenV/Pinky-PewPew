using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject menuObj;
    public AudioClip[] buttonSounds;
    public AudioClip pauseSound;

    void Start()
    {
        menuObj = GameObject.FindGameObjectWithTag("PauseMenu");
        menuObj.SetActive(false);
        paused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        SoundFXManager.instance.PlaySoundFXClip(pauseSound, transform, 1f);
        menuObj.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    
    public void ResumeGame()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(buttonSounds, transform, 1f);
        menuObj.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        PlayerPrefs.Save();
    }
    public void ToMenu()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(buttonSounds, transform, 1f);
        StartCoroutine(ToMenuDelay());
    }
    IEnumerator ToMenuDelay()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("Menu");
    }

}