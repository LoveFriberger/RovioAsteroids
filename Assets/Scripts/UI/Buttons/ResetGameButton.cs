using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResetGameButton : MonoBehaviour
{
    [Inject]
    PointsController pointsController = null;
    [Inject]
    GameManagerController gameController = null;

    public void ResetGame()
    {
        pointsController.Reset();
        gameController.InvokeResetGameAction();
    }
}
