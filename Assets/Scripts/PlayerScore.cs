using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public float score = 0;
    private TMP_Text scoreText;
    private TMP_Text highscoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>();
        highscoreText = GameObject.FindGameObjectWithTag("HighscoreText").GetComponent<TMP_Text>();
        UpdateScore();
        float loadedHighscore = PlayerPrefs.GetFloat("highScoreFloat");
        highscoreText.text = "Highscore: " + loadedHighscore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetScore(float gottenScore)
    {
        score += gottenScore;
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        float loadedHighscore = PlayerPrefs.GetFloat("highScoreFloat");
        if(score > loadedHighscore)
        {
            PlayerPrefs.SetFloat("highScoreFloat", score);
        }
    }
}
