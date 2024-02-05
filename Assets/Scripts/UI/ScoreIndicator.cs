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
    PointsController pointsController = null;


    int oldHighScore = 0;

    void Start()
    {
        oldHighScore = pointsController.HighScore();
        pointsController.AddOnScoreUpdatedAction(UpdateScoreIndicator);
        UpdateScoreIndicator();
    }

    private void OnDisable()
    {
        pointsController.RemoveOnScoreUpdatedAction(UpdateScoreIndicator);
    }

    void UpdateScoreIndicator()
    {
        scoreText.text = string.Format(scoreString, pointsController.CurrentPoints(), oldHighScore);
    }
}
