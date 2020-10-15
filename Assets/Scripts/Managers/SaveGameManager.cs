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
        SaveFile(1, -1);
        LoadFile();
    }  

    public void LoadFile()
    {
        // If highScoreKey & timeKey exists, Load into a HighScore and Time Text from UI
        if(PlayerPrefs.HasKey(highScoreKey) && PlayerPrefs.HasKey(timeKey))
        {
            scoreLabel = GameObject.FindWithTag("ScoreTime").GetComponent<TextMeshProUGUI>();
            scoreLabel.SetText(PlayerPrefs.GetInt(highScoreKey) + " " + PlayerPrefs.GetInt(timeKey));
        }
    }

    public void SaveFile(int highScore, int time)
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
