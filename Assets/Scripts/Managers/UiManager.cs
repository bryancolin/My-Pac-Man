using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public AudioManager backgroundMusic;
    private PacStudentController pacStudent;
    private TextMeshProUGUI playerScore, gameDurationTime, ghostScaredTime;

    private float playTime;
    private TimeSpan timePlaying;
    //private Image innerBar;
    //private Transform playerTransform;
    //private Camera camera;

    //private float yRotation;
    // Start is called before the first frame update
    private void Awake()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentGameState == GameManager.GameState.GameScene)
        {
            if (pacStudent != null)
            {
                playerScore.SetText(pacStudent.playerScore.ToString());
                BeginTimer();
            }
        }
    }

    public void BeginTimer()
    {
        playTime += Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(playTime);

        gameDurationTime.SetText(timePlaying.ToString("mm':'ss':'ff"));
    }

    private void LateUpdate()
    {

    }

    public void LoadFirstLevel()
    {
        if (GameManager.currentGameState == GameManager.GameState.StartScene)
        {
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
            SaveGameManager.SaveGame(pacStudent.playerScore, timePlaying.ToString("mm':'ss':'ff"));
            backgroundMusic.ChangeBackgroundMusic(0);
            playTime = 0;

            GameManager.currentGameState = GameManager.GameState.StartScene;
            SceneManager.LoadScene(0);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0)
        {
            GetStartSceneButton();
        }

        if (scene.buildIndex == 1)
        {
            GetExitButton();

            pacStudent = GameObject.FindWithTag("Player").GetComponent<PacStudentController>();
            playerScore = GameObject.FindWithTag("PlayerScore").GetComponent<TextMeshProUGUI>();
            gameDurationTime = GameObject.FindWithTag("GameDuration").GetComponent<TextMeshProUGUI>();
            ghostScaredTime = GameObject.FindWithTag("GhostTimer").GetComponent<TextMeshProUGUI>();
        }

        if(scene.buildIndex == 2)
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
}
