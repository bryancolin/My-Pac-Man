using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instances;

    public enum GameState { StartScene, GameScene, DesignScene }

    public static GameState currentGameState = GameState.StartScene;

    private GameObject pacStudent, redGhost, blueGhost, yellowGhost, pinkGhost;

    private AudioManager backgroundMusic;

    public bool isSetUp = false;
    private bool isScared = false;

    private float timer = 0;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {

    }

    private void Update()
    {
        if (currentGameState == GameState.GameScene)
        {
            if (SceneManager.GetSceneByName("GameScene").isLoaded)
            {
                if (!isSetUp)
                {
                    SetCamera();
                    SetGame();
                }
            }
        }
        //timer += Time.deltaTime;
        //if(isScared && timer <= 10)
        //{
        //    if(timer >=7)
        //    {
        //        RecoverGhost();
        //    }
        //}
        //else
        //{
        //    NormalGhost();
        //}
    }

    private void LateUpdate()
    {

    }

    void SetSingleton()
    {
        if (Instances == null)
        {
            DontDestroyOnLoad(gameObject);
            Instances = this;
        }
        else if (Instances != this)
        {
            Destroy(gameObject);
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

        redGhost = GameObject.FindWithTag("RedGhost");
        blueGhost = GameObject.FindWithTag("BlueGhost");
        yellowGhost = GameObject.FindWithTag("YellowGhost");
        pinkGhost = GameObject.FindWithTag("PinkGhost");

        backgroundMusic = GameObject.FindWithTag("Managers").GetComponent<AudioManager>();

        NormalGhost();

        isSetUp = true;
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


}
