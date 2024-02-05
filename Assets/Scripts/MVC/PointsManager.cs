using System.Collections;
using System;
using UnityEngine;

public class PointsModel
{
    public string playerPrefsHighScoreKey = "";
    public Action onScoreUpdated = null;
    public int currentPoints = 0;
    public bool newHighScore = false;
}

public class PointsViewer
{
    protected PointsModel model = new();

    public PointsViewer(string playerPrefsHighScoreKey)
    {
        model.playerPrefsHighScoreKey = playerPrefsHighScoreKey;
    }

    public int HighScore()
    {
        return PlayerPrefs.GetInt(model.playerPrefsHighScoreKey);
    }

    public int CurrentPoints()
    {
        return model.currentPoints;
    }

    public bool NewHighScore()
    {
        return model.newHighScore;
    }
}

public class PointsController : PointsViewer
{
    public PointsController(Settings settings) : base(settings.playerPrefsHighScoreKey) 
    {
    }

    public void SetNewHighScore(int newHighScore)
    {
        model.newHighScore = true;
        PlayerPrefs.SetInt(model.playerPrefsHighScoreKey, newHighScore);
    }

    public void AddPoints(int pointsToAdd)
    {
        model.currentPoints = model.currentPoints + pointsToAdd;
        if (model.currentPoints > HighScore())
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

    [Serializable]
    public class Settings
    {
        public string playerPrefsHighScoreKey = "";
    }
}
