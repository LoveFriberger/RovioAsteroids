using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HighScoreIndicator : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI highScoreText = null;

    [SerializeField]
    string highScoreString = "High Score: {0}";

    void Start()
    {
        var points = Core.Get<PointsManager>().HighScore;
        highScoreText.text = points > 0 ? string.Format(highScoreString, points) : "";
    }
}
