using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelUIMenuOpener : ITickable
{
    readonly LevelModel levelModel;
    readonly GameManagerController gameManagerController;
    readonly InputView inputView;
    readonly InputController inputController;

    public LevelUIMenuOpener(LevelModel levelModel, GameManagerController gameManagerController, InputView inputView, InputController inputController)
    {
        this.levelModel = levelModel;
        this.gameManagerController = gameManagerController;
        this.inputView = inputView;
        this.inputController = inputController;
    }

    public void OnPlayerKilled()
    {
        OpenMenu(true);
    }

    public void Tick()
    {
        if (inputView.ToggleMenuInputDown)
            ToggleMenu();
    }

    public void OpenMenu(bool died)
    {
        inputController.SetInputType(InputModel.Type.Menu);

        gameManagerController.SetPause(true);
        levelModel.PlayerDied = died;
        levelModel.MenuObjectActivated = true;
    }

    public void CloseMenu()
    {
        inputController.SetInputType(InputModel.Type.Player);
        gameManagerController.SetPause(false);
        levelModel.MenuObjectActivated = false;
    }

    public void ToggleMenu()
    {
        if (!levelModel.MenuObjectActivated)
            OpenMenu(false);
        else if (levelModel.CanCloseMenuWithKey)
            CloseMenu();
    }
}
