using UnityEngine;

public class PointsView
{
    readonly PointsModel model = new();

    public PointsView(PointsModel model)
    {
        this.model = model;
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
