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
    PointsController pointsController = null;

    void Start()
    {
        highScoreText.text = pointsController.HighScore() > 0 ? string.Format(highScoreString, pointsController.HighScore()) : "";
    }
}
