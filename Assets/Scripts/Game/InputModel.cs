public class InputModel
{
    public enum Type
    {
        Player,
        Menu
    }

    public Type inputType = Type.Menu;
    public bool upInputHold = false;
    public bool leftInputHold = false;
    public bool rightInputHold = false;
    public bool upInputDown = false;
    public bool downInputDown = false;
    public bool actionInputDown = false;
    public bool toggleMenuInputDown = false;
}
