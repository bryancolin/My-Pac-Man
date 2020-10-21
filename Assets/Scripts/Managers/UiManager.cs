using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public AudioManager backgroundMusic;
    private GameManager gameManager;

    private TextMeshProUGUI scoreLabel, playerScore, gameDurationTime, ghostScaredTime, gameOver;

    public List<Image> lives;
    private Image live1, live2, live3;

    private TimeSpan timePlaying;
    private float playTime, score;
    private bool isDisplay = false;

    // Start is called before the first frame update
    private void Awake()
    {
        SetSingleton();
    }

    void Start()
    {
        lives = new List<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.currentGameState)
        {
            case GameManager.GameState.GameScene:
                if (SceneManager.GetSceneByName("GameScene").isLoaded)
                    StartGame();
                break;

            case GameManager.GameState.GameOverScene:
                if(isDisplay == false)
                {
                    gameOver = GameObject.FindWithTag("GameOver").GetComponent<TextMeshProUGUI>();
                    gameOver.enabled = true;
                    isDisplay = true;
                }
                
                Invoke("ResetGame", 3.0f);
                break;
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
    }

    public void UpdateScoreLabel(int previousHighScore, TimeSpan timePlaying)
    {
        scoreLabel = GameObject.FindWithTag("ScoreTime").GetComponent<TextMeshProUGUI>();
        scoreLabel.SetText("High Score: " + previousHighScore + " - Time: " + timePlaying.ToString("mm':'ss':'ff"));
    }

    private void StartGame() 
    {
        // Score
        playerScore.SetText(score.ToString());

        // Time Playing
        playTime += Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(playTime);

        gameDurationTime.SetText(timePlaying.ToString("mm':'ss':'ff"));
    }

    private void Reset()
    {
        // Reset
        score = 0;
        playTime = 0;
        isDisplay = false;
        lives.Clear();

        // Change Music to Intro
        backgroundMusic.ChangeBackgroundMusic(0);

        // Set the Game State to Start
        GameManager.currentGameState = GameManager.GameState.StartScene;
        Destroy(this.gameObject.GetComponent<GameManager>());

        SceneManager.LoadScene(0);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void ResetGame()
    {
        //Time.timeScale = 0;
        if (GameManager.currentGameState == GameManager.GameState.GameOverScene)
        {
            // Save Game
            SaveGameManager.SaveGame((int) score, timePlaying.TotalSeconds);
            Reset();
        }
    }

    public void LoadFirstLevel()
    {
        if (GameManager.currentGameState == GameManager.GameState.StartScene)
        {
            backgroundMusic.StopPlaying();
            GameManager.currentGameState = GameManager.GameState.GameScene;

            SceneManager.LoadScene(1);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public void LoadDesignLevel()
    {

    }

    public void ExitGame()
    {
        if (GameManager.currentGameState == GameManager.GameState.GameScene)
        {
            Reset();
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            GetStartSceneButton();
        }

        if (scene.buildIndex == 1)
        {
            GetExitButton();            

            // Score, Games Duration and Ghost Scared Timer
            playerScore = GameObject.FindWithTag("PlayerScore").GetComponent<TextMeshProUGUI>();
            gameDurationTime = GameObject.FindWithTag("GameDuration").GetComponent<TextMeshProUGUI>();
            ghostScaredTime = GameObject.FindWithTag("GhostTimer").GetComponent<TextMeshProUGUI>();

            // Lives GameObject
            live1 = GameObject.Find("Lives 1").GetComponent<Image>();
            live2 = GameObject.Find("Lives 2").GetComponent<Image>();
            live3 = GameObject.Find("Lives 3").GetComponent<Image>();

            if (lives.Count < 3)
            {
                lives.Add(live1);
                lives.Add(live2);
                lives.Add(live3);
            } 
        }

        if (scene.buildIndex == 2)
        {

        }
    }

    public void GetStartSceneButton()
    {
        Button playButtom = GameObject.FindWithTag("PlayButton").GetComponent<Button>();
        playButtom.onClick.AddListener(LoadFirstLevel);

        Button designButton = GameObject.FindWithTag("DesignButton").GetComponent<Button>();
        designButton.onClick.AddListener(LoadDesignLevel);
    }

    public void GetExitButton()
    {
        Button exitButton = GameObject.FindWithTag("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(ExitGame);
    }

    void SetSingleton()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
