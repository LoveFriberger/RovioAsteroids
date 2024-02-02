using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject; 

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI titleText;

    [SerializeField]
    string newHighScoreString = "New High Score!";
    [SerializeField]
    string scoreString = "Better Luck Next Time!";
    [SerializeField]
    string pausedString = "Paused";


    [Inject]
    PointsController pointsController = null;

    public bool CanCloseWithKey { get; private set; }
    public void Setup(bool died)
    {
        CanCloseWithKey = !died;

        scoreText.text = pointsController.CurrentPoints().ToString();
        string titleString = pausedString;
        if (died)
            titleString = pointsController.NewHighScore() ? newHighScoreString : scoreString;

        titleText.text = titleString;
    }
}
