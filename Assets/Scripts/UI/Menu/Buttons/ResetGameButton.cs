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
        pointsController.Reset();
        if(invokeResetAction)
            gameController.InvokeResetGameAction();
    }
}
