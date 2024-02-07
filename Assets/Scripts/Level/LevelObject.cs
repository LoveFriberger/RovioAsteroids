using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject; 
public class LevelObject : MonoBehaviour
{
    InputController inputController;
    RockSpawner rockSpawner;
    PlayerSpawner playerSpawner;
    GameManagerController gameController;

    [Inject]
    public void Construct(InputController inputController, RockSpawner rockSpawner, PlayerSpawner playerSpawner, GameManagerController gameController)
    {
        this.inputController = inputController;
        this.rockSpawner = rockSpawner;
        this.playerSpawner = playerSpawner;
        this.gameController = gameController;
    }

    void Start()
    {
        inputController.SetInputType(InputModel.Type.Player);
        gameController.SetPause(false);
        gameController.AddResetGameAction(playerSpawner.SpawnPlayer);
        gameController.AddResetGameAction(rockSpawner.InstantiateStartRocks);
    }

    void OnDisable()
    {
        gameController.RemoveResetGameAction(playerSpawner.SpawnPlayer);
        gameController.RemoveResetGameAction(rockSpawner.InstantiateStartRocks);
    }
}
