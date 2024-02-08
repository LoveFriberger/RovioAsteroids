using UnityEngine;
using Zenject;

public class LevelUIObject : MonoBehaviour
{
    GameManagerController gameController;
    LevelUIMenuOpener levelUIMenuOpener;

    [Inject]
    public void Construct(GameManagerController gameController, LevelUIMenuOpener levelUIMenuOpener)
    {
        this.gameController = gameController;
        this.levelUIMenuOpener = levelUIMenuOpener;
    }

    void OnEnable()
    {
        gameController.AddPlayerKilledAction(levelUIMenuOpener.OnPlayerKilled);
        gameController.AddResetGameAction(levelUIMenuOpener.CloseMenu);
    }

    void OnDisable()
    {
        gameController.RemovePlayerKilledAction(levelUIMenuOpener.OnPlayerKilled);
        gameController.RemoveResetGameAction(levelUIMenuOpener.CloseMenu);
    }
}
