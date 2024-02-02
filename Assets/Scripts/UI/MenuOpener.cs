using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class MenuOpener : MonoBehaviour
{
    [SerializeField]
    GameMenu menuObject = null;

    [Inject]
    protected GameController gameController = null;

    void OnEnable()
    {
        gameController.AddOnPlayerKilledAction(OnPlayerKilled);
        gameController.AddResetGameAction(CloseMenu);
    }

    void OnDisable()
    {
        gameController.RemovePlayerKilledAction(OnPlayerKilled);
        gameController.RemoveResetGameAction(CloseMenu);
        SetPause(false);
    }

    void OnPlayerKilled()
    {
        OpenMenu(true);
    }

    public void OnToggleMenuInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (!menuObject.isActiveAndEnabled)
                OpenMenu(false);
            else if (menuObject.CanCloseWithKey)
                CloseMenu();
        }
    }

    public void OpenMenu(bool died)
    {
        SetPause(true);
        menuObject.gameObject.SetActive(true);
        menuObject.Setup(died);
    }

    public void CloseMenu()
    {
        SetPause(false);
        menuObject.gameObject.SetActive(false);
    }


    void SetPause(bool Pause)
    {
        Time.timeScale = Pause ? 0 : 1;
    }
}
