using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    int score;
    int totalScore;

    void Start() 
    {
        scoreText.text = 'x' + GetScore().ToString();
        score = GetScore();
    }

    void UpdateScore(int value)
    {
        score += value;
        if( score < 0)
            score = 0;
        
        totalScore = score;
        SetScore();

        scoreText.text = "x" + GetScore().ToString();
    }

    void OnEnable() 
    {
        CoinItem.collected += UpdateScore;
        PlayerController.dead += UpdateScore;
    }

    void OnDisable() 
    {
        CoinItem.collected -= UpdateScore;
        PlayerController.dead -= UpdateScore;
    }

    void SetScore() => PlayerPrefs.SetInt("Score", totalScore);

    int GetScore() => PlayerPrefs.GetInt("Score");
}
