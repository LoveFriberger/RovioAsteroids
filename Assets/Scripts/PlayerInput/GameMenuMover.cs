using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameMenuMover : MonoBehaviour
{
    [SerializeField]
    List<MenuButton> buttons = new();


    private void Start()
    {
        buttons[0].Select();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
            return;

        var movementInput = context.ReadValue<float>();

        if (movementInput == 0)
            return;

        var currentSelectedIndex = buttons.IndexOf(MenuButton.Selected);
        if (currentSelectedIndex < 0)
            return;

        if (movementInput > 0 && currentSelectedIndex > 0)
            buttons[currentSelectedIndex - 1].Select();
        else if (movementInput < 0 && currentSelectedIndex < buttons.Count - 1)
            buttons[currentSelectedIndex + 1].Select();
    }

    public void OnActionInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
            MenuButton.Selected.Click();
    }
    
}
