using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class ScoreIndicator : MonoBehaviour
{
    [SerializeField]
    string scoreString = "{0} ({1})";
    [SerializeField]
    TextMeshProUGUI scoreText = null;

    [Inject]
    PointsManager pointsManager = null;
    int oldHighScore = 0;

    void Start()
    {
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
