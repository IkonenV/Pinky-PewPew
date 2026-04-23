using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool paused;
    public GameObject menuObj;

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
        menuObj.SetActive(true);
        Time.timeScale = 0;
        paused = true;
    }

    
    public void ResumeGame()
    {
        menuObj.SetActive(false);
        Time.timeScale = 1;
        paused = false;
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}