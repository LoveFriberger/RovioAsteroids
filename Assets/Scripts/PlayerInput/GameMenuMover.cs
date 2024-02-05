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

        
    }

    public void OnActionInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
            MenuButton.Selected.Click();
    }
    
}
