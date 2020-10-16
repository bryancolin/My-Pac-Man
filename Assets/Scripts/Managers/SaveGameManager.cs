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

    public void LoadGame()
    {
        // If highScoreKey & timeKey exists, Load into a HighScore and Time Text from UI
        if(PlayerPrefs.HasKey(highScoreKey) && PlayerPrefs.HasKey(timeKey))
        {
            scoreLabel = GameObject.FindWithTag("ScoreTime").GetComponent<TextMeshProUGUI>();
            scoreLabel.SetText("High Score: " + PlayerPrefs.GetInt(highScoreKey) + " & Time: " + PlayerPrefs.GetInt(timeKey));
        }
    }

    public static void SaveGame(int highScore, int time)
    {
        // Save High Score and Time
        if (PlayerPrefs.GetInt(highScoreKey) < highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, highScore);
        }
        if (PlayerPrefs.GetInt(timeKey) > time)
        {
            PlayerPrefs.SetInt(timeKey, PlayerPrefs.GetInt(timeKey));
        }
        PlayerPrefs.Save();
    }
}
