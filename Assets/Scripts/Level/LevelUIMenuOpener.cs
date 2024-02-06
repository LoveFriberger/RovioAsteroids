using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelUIMenuOpener
{
    readonly LevelModel levelModel = null;
    readonly GameManagerController gameManagerController = null;
    readonly PointsView pointsView = null;
    readonly Settings settings = null;

    public LevelUIMenuOpener(LevelModel levelModel, GameManagerController gameManagerController, PointsView pointsView, Settings settings)
    {
        this.levelModel = levelModel;
        this.gameManagerController = gameManagerController;
        this.pointsView = pointsView;
        this.settings = settings;
    }

    public void OnPlayerKilled()
    {
        OpenMenu(true);
    }

    public void OpenMenu(bool died)
    {
        gameManagerController.SetPause(true);
        levelModel.PlayerDied = died;
        levelModel.MenuObjectActivated = true;

        levelModel.MenuDisplayedTitle = settings.pausedString;
        if (died)
            levelModel.MenuDisplayedTitle = pointsView.NewHighScore ? settings.newHighScoreString : settings.scoreString;

        levelModel.MenuDisplayedScore = pointsView.CurrentPoints.ToString();
    }

    public void CloseMenu()
    {
        gameManagerController.SetPause(false);
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
