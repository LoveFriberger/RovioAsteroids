using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuOpener : MonoBehaviour
{
    [SerializeField]
    GameObject menuObject = null;
    public void OnToggleMenuInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            var openMenu = !menuObject.activeSelf;
            SetPause(openMenu);
            menuObject.SetActive(openMenu);
        }
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
