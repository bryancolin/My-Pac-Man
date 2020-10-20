using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { StartScene, GameScene, DesignScene, GameOverScene }

    public static GameState currentGameState = GameState.StartScene;

    private GameObject pacStudent, redGhost, blueGhost, yellowGhost, pinkGhost;

    private AudioManager backgroundMusic;
    private UiManager uiManager;

    private bool isScared = false;

    private float timer = 0;
    private int lives;

    private void Awake()
    {
        backgroundMusic = GameObject.FindWithTag("Managers").GetComponent<AudioManager>();
        uiManager = GameObject.FindWithTag("Managers").GetComponent<UiManager>();

        if (currentGameState == GameState.GameScene)
        {
            if (SceneManager.GetSceneByName("GameScene").isLoaded)
            {
                lives = 3;
                SetCamera();
                SetGame();
            }
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(lives == 0)
        {
            currentGameState = GameState.GameOverScene;
        }

        switch (currentGameState)
        {
            case GameState.GameScene:
                break;

            case GameState.GameOverScene:
                ResetGame();
                break;
        }
    }

    // Set Camera for Game Scene
    private void SetCamera()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 15.0f;
    }

    // Get GameObject Character and Audio Manager
    private void SetGame()
    {
        pacStudent = GameObject.FindWithTag("Player");
        pacStudent.GetComponent<PacStudentController>().enabled = true;

        redGhost = GameObject.FindWithTag("RedGhost");
        blueGhost = GameObject.FindWithTag("BlueGhost");
        yellowGhost = GameObject.FindWithTag("YellowGhost");
        pinkGhost = GameObject.FindWithTag("PinkGhost");

        redGhost.GetComponent<GhostMovement>().enabled = true;
        blueGhost.GetComponent<GhostMovement>().enabled = true;
        yellowGhost.GetComponent<GhostMovement>().enabled = true;
        pinkGhost.GetComponent<GhostMovement>().enabled = true;

        NormalGhost();
    }

    private void ResetGame()
    {
        pacStudent.GetComponent<Animator>().SetFloat("Speed", 0);

        redGhost.GetComponent<Animator>().SetFloat("Speed", 0);
        blueGhost.GetComponent<Animator>().SetFloat("Speed", 0);
        yellowGhost.GetComponent<Animator>().SetFloat("Speed", 0);
        pinkGhost.GetComponent<Animator>().SetFloat("Speed", 0);
    }

    public void NormalGhost()
    {
        redGhost.GetComponent<GhostMovement>().SetGhost();
        blueGhost.GetComponent<GhostMovement>().SetGhost();
        yellowGhost.GetComponent<GhostMovement>().SetGhost();
        pinkGhost.GetComponent<GhostMovement>().SetGhost();

        if (isScared)
        {
            redGhost.GetComponent<GhostMovement>().SetNormal();
            blueGhost.GetComponent<GhostMovement>().SetNormal();
            yellowGhost.GetComponent<GhostMovement>().SetNormal();
            pinkGhost.GetComponent<GhostMovement>().SetNormal();
        }

        backgroundMusic.ChangeBackgroundMusic(1);
    }

    public void ScareGhost()
    {
        isScared = true;

        redGhost.GetComponent<GhostMovement>().SetScared();
        blueGhost.GetComponent<GhostMovement>().SetScared();
        yellowGhost.GetComponent<GhostMovement>().SetScared();
        pinkGhost.GetComponent<GhostMovement>().SetScared();

        backgroundMusic.ChangeBackgroundMusic(2);

        timer = 0.0f;
    }

    public void RecoverGhost()
    {
        redGhost.GetComponent<GhostMovement>().SetTransition();
        blueGhost.GetComponent<GhostMovement>().SetTransition();
        yellowGhost.GetComponent<GhostMovement>().SetTransition();
        pinkGhost.GetComponent<GhostMovement>().SetTransition();
    }

    public void LoseLife()
    {
        lives--;
        Destroy(uiManager.lives[lives]);
        uiManager.lives.RemoveAt(lives);
    }

}
