using System.Collections;
using System;
using UnityEngine;

public class PointsManager : Manager
{
    string playerPrefsHighScoreKey = "HighScore";

    public Action OnScoreUpdated = null;

    public int CurrentPoints { get; private set; }
    public bool NewHighScore { get; private set; }


    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(playerPrefsHighScoreKey);
        }
        set
        {
            NewHighScore = true;
            PlayerPrefs.SetInt(playerPrefsHighScoreKey, value);
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        CurrentPoints = CurrentPoints + pointsToAdd;
        if (CurrentPoints > HighScore)
            HighScore = CurrentPoints;

        OnScoreUpdated.Invoke();
    }

    public void Reset()
    {
        CurrentPoints = 0;
        NewHighScore = false;
    }
}
