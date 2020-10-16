using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instances;

    public enum GameState { StartScene, GameScene, DesignScene }

    public static GameState currentGameState = GameState.StartScene;

    private void Awake()
    {
        SetSingleton();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (currentGameState == GameState.GameScene)
            SetCamera();
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

    private void SetCamera()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 15.0f;
    }
}
