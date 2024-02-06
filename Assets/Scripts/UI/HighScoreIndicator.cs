using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class HighScoreIndicator : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScoreText = null;

    [SerializeField]
    string highScoreString = "High Score: {0}";

    [Inject]
    PointsView pointsView = null;

    void Start()
    {
        highScoreText.text = pointsView.HighScore > 0 ? string.Format(highScoreString, pointsView.HighScore) : "";
    }
}
