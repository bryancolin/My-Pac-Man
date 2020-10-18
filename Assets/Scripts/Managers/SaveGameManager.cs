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

    private TextMeshProUGUI scoreLabel;

    void Awake()
    {
        LoadGame();
    }  

    // Load Save File to the Game
    public void LoadGame()
    {
        // If highScoreKey & timeKey exists, Load into a HighScore and Time Text from UI
        if(PlayerPrefs.HasKey(highScoreKey) && PlayerPrefs.HasKey(timeKey))
        {
            scoreLabel = GameObject.FindWithTag("ScoreTime").GetComponent<TextMeshProUGUI>();
            scoreLabel.SetText("High Score: " + PlayerPrefs.GetInt(highScoreKey) + " - Time: " + PlayerPrefs.GetFloat(timeKey));
        }
    }

    // Save High Score and Time to the File
    public static void SaveGame(int highScore, double time)
    {
        // If highschore is higher than previous high score
        if (PlayerPrefs.GetInt(highScoreKey) < highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, highScore);

            // If the time playing is lower than previous time playing
            if (PlayerPrefs.GetFloat(timeKey) > time)
            {
                PlayerPrefs.GetFloat(timeKey, (float) time);
            }
        }
        PlayerPrefs.Save();
    }
}
