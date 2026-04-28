using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip[] buttonSounds;
    public GameObject settingsObject;
    public GameObject menuObject;
    public AudioMixer audioMixer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;

        float masterV = PlayerPrefs.GetFloat("MasterVol", 0.75f);
        audioMixer.SetFloat("masterVolume", Mathf.Log10(masterV) * 20f);

        float musicV = PlayerPrefs.GetFloat("MusicVol", 0.75f);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicV) * 20f);

        float soundFXV = PlayerPrefs.GetFloat("SoundFXVol", 0.75f);
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(soundFXV) * 20f);
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
        PlayerPrefs.Save();
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("SampleScene");
    }
}