using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameState currentGameState = GameState.StartScene;

    private AudioManager backgroundMusic;
    private UiManager uiManager;

    private PacStudentController pacStudent;
    private GhostController redGhost, blueGhost, yellowGhost, pinkGhost;

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
            if (SceneManager.GetSceneByName("InnovativeScene").isLoaded)
            {
                lives = 3;
                SetCharacter();
            }
        }
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GameState.GameScene:
                MovementBGM();

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
        pacStudent = GameObject.FindWithTag("Player").GetComponent<PacStudentController>();
        redGhost = GameObject.FindWithTag("RedGhost").GetComponent<GhostController>();
        blueGhost = GameObject.FindWithTag("BlueGhost").GetComponent<GhostController>();
        yellowGhost = GameObject.FindWithTag("YellowGhost").GetComponent<GhostController>();
        pinkGhost = GameObject.FindWithTag("PinkGhost").GetComponent<GhostController>();

        pacStudent.enabled = true;
        redGhost.enabled = true;
        blueGhost.enabled = true;
        yellowGhost.enabled = true;
        pinkGhost.enabled = true;

        backgroundMusic.ChangeBackgroundMusic(1);
    }

    private void MovementBGM()
    {
        if(!backgroundMusic.Playing())
        {
            if(redGhost.currentGhostState == GhostState.Normal && blueGhost.currentGhostState == GhostState.Normal && yellowGhost.currentGhostState == GhostState.Normal && pinkGhost.currentGhostState == GhostState.Normal)
            {
                backgroundMusic.ChangeBackgroundMusic(1);
            }
            else
            {
                if ((redGhost.currentGhostState == GhostState.Scared || blueGhost.currentGhostState == GhostState.Scared || yellowGhost.currentGhostState == GhostState.Scared || pinkGhost.currentGhostState == GhostState.Scared) || (redGhost.currentGhostState == GhostState.Recovering || blueGhost.currentGhostState == GhostState.Recovering || yellowGhost.currentGhostState == GhostState.Recovering || pinkGhost.currentGhostState == GhostState.Recovering))
                {
                    if (redGhost.currentGhostState == GhostState.Death || blueGhost.currentGhostState == GhostState.Death || yellowGhost.currentGhostState == GhostState.Death || pinkGhost.currentGhostState == GhostState.Death)
                    {
                        backgroundMusic.ChangeBackgroundMusic(3);
                    }
                    else
                    {
                        backgroundMusic.ChangeBackgroundMusic(2);
                    }
                }
            }
        }
    }

    public void ScareGhost()
    {
        redGhost.SetScared();
        blueGhost.SetScared();
        yellowGhost.SetScared();
        pinkGhost.SetScared();
    }

    public void LoseLife()
    {
        StartCoroutine(pacStudent.DeadTrigger());

        lives--;
        Destroy(uiManager.lives[lives]);
        uiManager.lives.RemoveAt(lives);
    }

}
