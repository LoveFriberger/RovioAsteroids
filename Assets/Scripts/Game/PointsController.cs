using System;
using UnityEngine;

public class PointsController
{
    readonly PointsModel model = null;
    readonly PointsView view = null;

    public PointsController(PointsModel model, PointsView view)
    {
        this.model = model;
        this.view = view;
    }

    public void SetNewHighScore(int newHighScore)
    {
        model.newHighScore = true;
        PlayerPrefs.SetInt(model.playerPrefsHighScoreKey, newHighScore);
    }

    public void AddPoints(int pointsToAdd)
    {
        model.currentPoints = model.currentPoints + pointsToAdd;
        if (model.currentPoints > view.HighScore())
            SetNewHighScore(model.currentPoints);

        model.onScoreUpdated.Invoke();
    }

    public void Reset()
    {
        model.currentPoints = 0;
        model.newHighScore = false;

        model.onScoreUpdated?.Invoke();
    }

    public void AddOnScoreUpdatedAction(Action action)
    {
        model.onScoreUpdated += action;
    }

    public void RemoveOnScoreUpdatedAction(Action action)
    {
        model.onScoreUpdated -= action;
    }
}
