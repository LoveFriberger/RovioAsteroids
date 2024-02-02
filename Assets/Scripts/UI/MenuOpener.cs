using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOpener : MonoBehaviour
{
    [SerializeField]
    GameMenu menuObject = null;

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

    private void OnDisable()
    {
        SetPause(false);
    }
}
