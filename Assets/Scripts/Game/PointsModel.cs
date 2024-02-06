using System;

public class PointsModel
{
    public string playerPrefsHighScoreKey = "HighScore";
    public Action onScoreUpdated = null;
    public int currentPoints = 0;
    public bool newHighScore = false;
}
