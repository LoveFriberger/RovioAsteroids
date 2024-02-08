using UnityEngine;
using TMPro;
using Zenject;

public class HighScoreIndicator : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScoreText;

    [SerializeField]
    string highScoreString = "High Score: {0}";

    [Inject]
    PointsView pointsView;

    [Inject]
    void Start()
    {
        highScoreText.text = pointsView.HighScore > 0 ? string.Format(highScoreString, pointsView.HighScore) : "";
        Debug.Log(string.Format("Changed high score text to \"{0}\"", highScoreText.text));
    }
}
