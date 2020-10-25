using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    StartScene,
    GameScene,
    DesignScene,
    GameOverScene
}

public enum GhostState
{
    Normal,
    Scared,
    Recovering,
    Death
}