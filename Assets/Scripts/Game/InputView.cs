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
