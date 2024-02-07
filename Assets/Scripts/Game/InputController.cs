using UnityEngine;
using Zenject;

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

    public void SetInputType(InputModel.Type type)
    {
        model.inputType = type;
    }
}
