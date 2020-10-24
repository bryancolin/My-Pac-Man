using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState { StartScene, GameScene, DesignScene, GameOverScene }

    public static GameState currentGameState = GameState.StartScene;

    private AudioManager backgroundMusic;
    private UiManager uiManager;
  
    private GameObject pacStudent, redGhost, blueGhost, yellowGhost, pinkGhost;

    private bool isScared = false;

    private int lives;

    private void Awake()
    {
        backgroundMusic = GameObject.FindWithTag("Managers").GetComponent<AudioManager>();
        uiManager = GameObject.FindWithTag("Managers").GetComponent<UiManager>();
    }

    private void Start()
    {
        if (currentGameState == GameState.GameScene)
        {
            if (SceneManager.GetSceneByName("GameScene").isLoaded)
            {
                lives = 3;
                SetCamera();
                SetCharacter();
            }
        }
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.GameScene:
                if(lives == 0 || pacStudent.GetComponent<PacStudentController>().totalPellets == 222)
                {
                    currentGameState = GameState.GameOverScene;
                }
                break;

            case GameState.GameOverScene:
                break;
        }
    }

    // Set Camera for Game Scene
    private void SetCamera()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 15.0f;
    }

    // Get GameObject Character
    private void SetCharacter()
    {
        pacStudent = GameObject.FindWithTag("Player");
        redGhost = GameObject.FindWithTag("RedGhost");
        blueGhost = GameObject.FindWithTag("BlueGhost");
        yellowGhost = GameObject.FindWithTag("YellowGhost");
        pinkGhost = GameObject.FindWithTag("PinkGhost");

        pacStudent.GetComponent<PacStudentController>().enabled = true;
        redGhost.GetComponent<GhostController>().enabled = true;
        blueGhost.GetComponent<GhostController>().enabled = true;
        yellowGhost.GetComponent<GhostController>().enabled = true;
        pinkGhost.GetComponent<GhostController>().enabled = true;

        NormalGhost();
    }

    public void NormalGhost()
    {
        if (isScared)
        {
            redGhost.GetComponent<GhostController>().SetNormal();
            blueGhost.GetComponent<GhostController>().SetNormal();
            yellowGhost.GetComponent<GhostController>().SetNormal();
            pinkGhost.GetComponent<GhostController>().SetNormal();
        }

        backgroundMusic.ChangeBackgroundMusic(1);
    }

    public void ScareGhost()
    {
        isScared = true;

        redGhost.GetComponent<GhostController>().SetScared();
        blueGhost.GetComponent<GhostController>().SetScared();
        yellowGhost.GetComponent<GhostController>().SetScared();
        pinkGhost.GetComponent<GhostController>().SetScared();
    }

    public void LoseLife()
    {
        lives--;
        Destroy(uiManager.lives[lives]);
        uiManager.lives.RemoveAt(lives);
    }

}
