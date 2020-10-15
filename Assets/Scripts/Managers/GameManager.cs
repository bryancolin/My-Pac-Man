using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { StartScene, GameScene, DesignScene }

    public static GameState currentGameState = GameState.StartScene;
}
