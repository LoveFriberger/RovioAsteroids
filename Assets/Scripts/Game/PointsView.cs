using UnityEngine;

public class PointsView
{
    readonly PointsModel model = new();

    public PointsView(PointsModel model)
    {
        this.model = model;
    }

    public int HighScore { get { return PlayerPrefs.GetInt(model.playerPrefsHighScoreKey); } }

    public int CurrentPoints { get { return model.currentPoints; } }

    public bool NewHighScore { get { return model.newHighScore; } }
}
