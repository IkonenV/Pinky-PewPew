using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] buttonSounds;
    public GameObject settingsObject;
    public GameObject menuObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(buttonSounds, transform, 0.5f);
        StartCoroutine(LoadScene());
    }
    public void OpenSettings()
    { 
        SoundFXManager.instance.PlayRandomSoundFXClip(buttonSounds, transform, 0.5f);
        settingsObject.SetActive(true);
        menuObject.SetActive(false);
    }
    public void CloseSettings()
    {
        SoundFXManager.instance.PlayRandomSoundFXClip(buttonSounds, transform, 0.5f);
        settingsObject.SetActive(false);
        menuObject.SetActive(true);
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("SampleScene");
    }
}