using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputModel
{
    public bool upInputHold = false;
    public bool leftInputHold = false;
    public bool rightInputHold = false;
    public bool upInputDown = false;
    public bool downInputDown = false;
    public bool actionInputDown = false;
    public bool toggleMenuInputDown = false;
}

public class InputView
{
    InputModel model;

    public InputView(InputModel model) 
    {
        this.model = model;
    }

    public bool UpInputHold { get { return model.upInputHold; } }

    public bool LeftInputHold { get { return model.leftInputHold; } }

    public bool RightInputHold { get { return model.rightInputHold; } }

    public bool UpInputDown { get { return model.upInputDown; } }

    public bool DownInputDown { get { return model.downInputDown; } }

    public bool ActionInputDown { get { return model.actionInputDown; } }

    public bool ToggleMenuInputDown { get { return model.toggleMenuInputDown; } }
}

public class InputController : ITickable
{
    InputModel model;

    public InputController(InputModel model) 
    {
        this.model = model;
    }

    public void Tick()
    {
        model.upInputHold = Input.GetKey(KeyCode.UpArrow);
        model.leftInputHold = Input.GetKey(KeyCode.LeftArrow);
        model.rightInputHold = Input.GetKey(KeyCode.RightArrow);
        model.upInputDown = Input.GetKeyDown(KeyCode.UpArrow);
        model.downInputDown = Input.GetKeyDown(KeyCode.DownArrow);
        model.actionInputDown = Input.GetKeyDown(KeyCode.Space);
        model.toggleMenuInputDown = Input.GetKeyDown(KeyCode.Escape);
    }

}
