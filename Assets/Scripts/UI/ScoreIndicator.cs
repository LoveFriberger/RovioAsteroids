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
    [Inject]
    GameManagerController gameManagerController;

    int oldHighScore = 0;

    [Inject]
    void Start()
    {
        pointsController.AddScoreUpdatedAction(UpdateScoreIndicator);
        gameManagerController.AddResetGameAction(ResetScoreIndicator);
        ResetScoreIndicator();
    }

    void OnDisable()
    {
        pointsController.RemoveScoreUpdatedAction(UpdateScoreIndicator);
        gameManagerController.RemoveResetGameAction(ResetScoreIndicator);
    }

    void ResetScoreIndicator()
    {
        oldHighScore = pointsView.HighScore;
        UpdateScoreIndicator();
    }

    void UpdateScoreIndicator()
    {
        if(scoreText != null)
            scoreText.text = string.Format(scoreString, pointsView.CurrentPoints, oldHighScore);

        Debug.Log(string.Format("Updated score indicator to \"{0}\"", scoreText.text));
    }
}
