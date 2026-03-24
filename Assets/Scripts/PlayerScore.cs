using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public float score = 0;
    private TMP_Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetScore(float gottenScore)
    {
        score += gottenScore;
    }
}
