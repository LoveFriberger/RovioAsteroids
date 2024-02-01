using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : Manager
{
    string playerPrefsPointsKey = "points";

    public int Points
    {
        get
        {
            return PlayerPrefs.GetInt(playerPrefsPointsKey);
        }
    }

    public void AddPoints(int pointsToAdd)
    {
        var newPoints = Points + pointsToAdd;
        PlayerPrefs.SetInt(playerPrefsPointsKey, newPoints);
    }
}
