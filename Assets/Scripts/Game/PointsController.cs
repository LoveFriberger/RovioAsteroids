using System;
using UnityEngine;

public class PointsController
{
    readonly PointsModel model;
    readonly PointsView view;

    public PointsController(PointsModel model, PointsView view)
    {
        this.model = model;
        this.view = view;
    }

    public void NewHighScore(int newHighScore)
    {
        model.newHighScore = true;
        PlayerPrefs.SetInt(model.playerPrefsHighScoreKey, newHighScore); 
        Debug.Log(string.Format("New high score set, {0}", newHighScore));
    }

    public void AddPoints(int pointsToAdd)
    {
        model.currentPoints = model.currentPoints + pointsToAdd;

        Debug.Log(string.Format("Points added, new score {0}", pointsToAdd));

        if (model.currentPoints > view.HighScore)
            NewHighScore(model.currentPoints);

        model.onScoreUpdated?.Invoke();
    }

    public void Reset()
    {
        model.currentPoints = 0;
        model.newHighScore = false;

        Debug.Log("Points reset");

        model.onScoreUpdated?.Invoke();
    }

    public void AddScoreUpdatedAction(Action action)
    {
        model.onScoreUpdated += action;
    }

    public void RemoveScoreUpdatedAction(Action action)
    {
        model.onScoreUpdated -= action;
    }
}
