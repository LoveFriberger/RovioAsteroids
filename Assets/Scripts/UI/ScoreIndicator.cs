using UnityEngine;
using TMPro;
using Zenject;

public class ScoreIndicator : MonoBehaviour
{
    [SerializeField]
    string scoreString = "{0} ({1})";
    [SerializeField]
    TextMeshProUGUI scoreText;

    [Inject]
    PointsView pointsView;
    [Inject]
    PointsController pointsController;

    int oldHighScore = 0;

    [Inject]
    void Start()
    {
        oldHighScore = pointsView.HighScore;
        pointsController.AddScoreUpdatedAction(UpdateScoreIndicator);
        UpdateScoreIndicator();
    }

    private void OnDisable()
    {
        pointsController.RemoveScoreUpdatedAction(UpdateScoreIndicator);
    }

    void UpdateScoreIndicator()
    {
        if(scoreText != null)
            scoreText.text = string.Format(scoreString, pointsView.CurrentPoints, oldHighScore);

        Debug.Log(string.Format("Updated score indicator to \"{0}\"", scoreText.text));
    }
}
