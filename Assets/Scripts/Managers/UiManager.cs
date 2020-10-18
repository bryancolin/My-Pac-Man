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
    private TextMeshProUGUI playerScore, gameDurationTime, ghostScaredTime, countdownDisplay;

    private float playTime;
    private TimeSpan timePlaying;

    private int countdownTime = 3;
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
            if (SceneManager.GetSceneByName("GameScene").isLoaded)
            {
                if (pacStudent != null)
                {
                    BeginGame();
                    //if (Time.timeScale == 0)
                    //{
                    //    StartCoroutine(CountdownToStart());
                    //}
                }
            }
        }
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.SetText(countdownTime.ToString());
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.SetText("GO!");
        yield return new WaitForSeconds(1f);
        //Time.timeScale = 1;
        countdownDisplay.gameObject.SetActive(false);
    }

    private void BeginGame()
    {
        playerScore.SetText(pacStudent.playerScore.ToString());

        playTime += Time.deltaTime;
        timePlaying = TimeSpan.FromSeconds(playTime);

        gameDurationTime.SetText(timePlaying.ToString("mm':'ss':'ff"));
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
            SaveGameManager.SaveGame(pacStudent.playerScore, timePlaying.TotalMinutes);
            backgroundMusic.ChangeBackgroundMusic(0);
            playTime = 0;

            GameManager.currentGameState = GameManager.GameState.StartScene;
            SceneManager.LoadScene(0);
            SceneManager.sceneLoaded += OnSceneLoaded;
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

            pacStudent = GameObject.FindWithTag("Player").GetComponent<PacStudentController>();
            playerScore = GameObject.FindWithTag("PlayerScore").GetComponent<TextMeshProUGUI>();
            gameDurationTime = GameObject.FindWithTag("GameDuration").GetComponent<TextMeshProUGUI>();
            ghostScaredTime = GameObject.FindWithTag("GhostTimer").GetComponent<TextMeshProUGUI>();

            countdownDisplay = GameObject.FindWithTag("CountDown").GetComponent<TextMeshProUGUI>();
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
}
