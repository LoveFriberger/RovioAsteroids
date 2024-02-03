using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InputModel
{
    public bool upInput = false;
    public bool downInput = false; 
    public bool leftInput = false;
    public bool rightInput = false;
    public bool actionInput = false;
    public bool backInput = false;
}

public class InputView
{
    InputModel model;

    public InputView(InputModel model) 
    {
        this.model = model;
    }

    public bool UpInput { get { return model.upInput; } }

    public bool DownInput { get { return model.downInput; } }
    public bool LeftInput { get { return model.leftInput; } }

    public bool RightInput { get { return model.rightInput; } }

    public bool ActionInput { get { return model.actionInput; } }

    public bool BackInput { get { return model.backInput; } }
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
        model.upInput = Input.GetKey(KeyCode.UpArrow);
        model.downInput = Input.GetKey(KeyCode.DownArrow);
        model.leftInput = Input.GetKey(KeyCode.LeftArrow);
        model.rightInput = Input.GetKey(KeyCode.RightArrow);
        model.actionInput = Input.GetKey(KeyCode.Space);
        model.backInput = Input.GetKey(KeyCode.Escape);
    }

}
