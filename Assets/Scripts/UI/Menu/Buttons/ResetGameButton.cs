using UnityEngine;
using Zenject;

public class ResetGameButton : MonoBehaviour
{
    [Inject]
    PointsController pointsController = null;
    [Inject]
    GameManagerController gameController = null;

    public void ResetGame(bool invokeResetAction)
    {
        pointsController.ResetGamePoints();
        if(invokeResetAction)
            gameController.InvokeResetGameAction();
    }
}
