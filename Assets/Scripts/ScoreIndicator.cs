using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreIndicator : MonoBehaviour
{
    [SerializeField]
    string scoreString = "{0} ({1})";
    [SerializeField]
    TextMeshProUGUI scoreText = null;


    PointsManager pointsManager = null;
    int oldHighScore = 0;

    void Start()
    {
        pointsManager = Core.Get<PointsManager>();
        oldHighScore = pointsManager.HighScore;
        pointsManager.OnScoreUpdated += UpdateScoreIndicator;
        UpdateScoreIndicator();
    }

    private void OnDisable()
    {
        pointsManager.OnScoreUpdated -= UpdateScoreIndicator;
    }

    void UpdateScoreIndicator()
    {
        scoreText.text = string.Format(scoreString, pointsManager.CurrentPoints, oldHighScore);
    }
}
