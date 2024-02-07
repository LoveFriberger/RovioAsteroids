using System.Collections;
using System;
using UnityEngine;
using TMPro;
using Zenject;

public class GameMenuTitleText : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI titleText = null;

    [Inject]
    Settings settings;
    [Inject]
    LevelModel levelModel;
    [Inject]
    PointsView pointsView;

    void Start()
    {
        titleText.text = settings.pausedString;
        if (levelModel.PlayerDied)
            titleText.text = pointsView.NewHighScore ? settings.newHighScoreString : settings.scoreString;
    }

    [Serializable]
    public class Settings
    {
        public string newHighScoreString = "New High Score!";
        public string scoreString = "Better Luck Next Time!";
        public string pausedString = "Paused";
    }
}
