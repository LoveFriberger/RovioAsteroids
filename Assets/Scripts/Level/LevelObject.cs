using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject; 
public class LevelObject : MonoBehaviour
{
    LevelModel level = null;
    RockSpawner rockSpawner = null;
    PlayerSpawner playerSpawner = null;
    GameController gameController = null;

    [Inject]
    public void Construct(LevelModel level, RockSpawner rockSpawner, PlayerSpawner playerSpawner, GameController gameController)
    {
        this.level = level;
        this.rockSpawner = rockSpawner;
        this.playerSpawner = playerSpawner;
        this.gameController = gameController;
    }

    void Start()
    {
        gameController.SetPause(false);
        gameController.AddResetGameAction(playerSpawner.SpawnPlayer);
        gameController.AddResetGameAction(rockSpawner.InstantiateStartRocks);
    }

    void OnDisable()
    {
        gameController.AddResetGameAction(playerSpawner.SpawnPlayer);
        gameController.AddResetGameAction(rockSpawner.InstantiateStartRocks);
    }
}
