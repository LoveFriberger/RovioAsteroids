using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelUIObject : MonoBehaviour
{
    LevelModel levelModel = null;
    GameController gameController = null;
    LevelUIMenuOpener levelUIMenuOpener = null;

    [Inject]
    public void Construct(LevelModel levelModel, GameController gameController, LevelUIMenuOpener levelUIMenuOpener)
    {
        this.levelModel = levelModel;
        this.gameController = gameController;
        this.levelUIMenuOpener = levelUIMenuOpener;
    }

    void OnEnable()
    {
        gameController.AddOnPlayerKilledAction(levelUIMenuOpener.OnPlayerKilled);
        gameController.AddResetGameAction(levelUIMenuOpener.CloseMenu);
    }

    void OnDisable()
    {
        gameController.RemovePlayerKilledAction(levelUIMenuOpener.OnPlayerKilled);
        gameController.RemoveResetGameAction(levelUIMenuOpener.CloseMenu);
        gameController.SetPause(false);
    }
}
