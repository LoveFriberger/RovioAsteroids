using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class LevelUIMenuOpener
{
    readonly LevelModel levelModel = null;
    readonly GameController gameController = null;
    readonly PointsController pointsController = null;
    readonly InputView inputView = null;
    readonly Settings settings = null;

    public LevelUIMenuOpener(LevelModel levelModel, GameController gameController, PointsController pointsController, InputView inputView, Settings settings)
    {
        this.levelModel = levelModel;
        this.gameController = gameController;
        this.pointsController = pointsController;
        this.inputView = inputView;
        this.settings = settings;
    }

    public void OnPlayerKilled()
    {
        OpenMenu(true);
    }

    public void OpenMenu(bool died)
    {
        inputView.InGameMenu = true;

        gameController.SetPause(true);
        levelModel.PlayerDied = died;
        levelModel.MenuObjectActivated = true;

        levelModel.MenuDisplayedTitle = settings.pausedString;
        if (died)
            levelModel.MenuDisplayedTitle = pointsController.NewHighScore() ? settings.newHighScoreString : settings.scoreString;

        levelModel.MenuDisplayedScore = pointsController.CurrentPoints().ToString();

        levelModel.MenuButtons[0].Select();
    }

    public void CloseMenu()
    {
        inputView.InGameMenu = false;
        gameController.SetPause(false);
        levelModel.MenuObjectActivated = false;
    }

    [Serializable]
    public class Settings
    {
        public string newHighScoreString = "New High Score!";
        public string scoreString = "Better Luck Next Time!";
        public string pausedString = "Paused";
    }
}
