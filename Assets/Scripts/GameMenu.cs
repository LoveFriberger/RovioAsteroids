using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public bool CanCloseWithKey { get; private set; }
    public void Setup(bool died)
    {
        CanCloseWithKey = !died;

        scoreText.text = Core.Get<PointsManager>().CurrentPoints.ToString();
        string titleString = pausedString;
        if (died)
            titleString = Core.Get<PointsManager>().NewHighScore ? newHighScoreString : scoreString;

        titleText.text = titleString;
    }
}
