using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SaveGameManager : MonoBehaviour
{
    private const string highScoreKey = "HighScore";
    private const string timeKey = "Time";

    private static int previousHighScore;
    private static float previousTimePlay;

    private TimeSpan timePlaying;

    private TextMeshProUGUI scoreLabel;

    void Awake()
    {
        previousHighScore = PlayerPrefs.GetInt(highScoreKey);
        previousTimePlay = PlayerPrefs.GetFloat(timeKey);
        timePlaying = TimeSpan.FromSeconds(previousTimePlay);

        LoadGame();
    }  

    // Load Save File to the Game
    public void LoadGame()
    {
        // If highScoreKey & timeKey exists, Load into a HighScore and Time Text from UI
        if(PlayerPrefs.HasKey(highScoreKey) && PlayerPrefs.HasKey(timeKey))
        {
            scoreLabel = GameObject.FindWithTag("ScoreTime").GetComponent<TextMeshProUGUI>();
            scoreLabel.SetText("High Score: " + previousHighScore + " - Time: " + timePlaying.ToString("mm':'ss':'ff"));
        }
    }

    // Save High Score and Time Playing Duration to the File
    public static void SaveGame(int highScore, double time)
    {
        // If current highschore is equal to previous high score
        if (previousHighScore == highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, highScore);

            // If current time playing is lower than previous time playing
            if (PlayerPrefs.GetFloat(timeKey) > time)
                PlayerPrefs.SetFloat(timeKey, (float) time);
        }
        else if(previousHighScore < highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.SetFloat(timeKey, (float) time);
        }

        PlayerPrefs.Save();
    }
}
