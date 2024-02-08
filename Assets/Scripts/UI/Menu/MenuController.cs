using UnityEngine;
using Zenject;

public class MenuController : ITickable
{
    readonly MenuModel mainMenuModel;
    readonly InputView inputView ;

    public MenuController(MenuModel mainMenuModel, InputView inputView)
    {
        this.mainMenuModel = mainMenuModel;
        this.inputView = inputView;
    }

    public void Tick()
    {
        if (inputView.InputType != InputModel.Type.Menu)
            return; 

        if (mainMenuModel.SelectedButtonIndex == -1)
            mainMenuModel.MenuButtons[0].Select();

        if (inputView.UpInputDown)
            ChangeSelectedButton(true);
        if (inputView.DownInputDown)
            ChangeSelectedButton(false);
        if (inputView.ActionInputDown)
            MenuButton.Selected.Click();
    }
    void ChangeSelectedButton(bool up)
    {
        Debug.Log(string.Format("Moving menu selection {0}", up ? "up" : "down"));
        var currentSelectedIndex = mainMenuModel.SelectedButtonIndex;
        if (currentSelectedIndex < 0)
            return;

        if (!up && currentSelectedIndex < mainMenuModel.MenuButtons.Count - 1)
            mainMenuModel.MenuButtons[currentSelectedIndex + 1].Select();

        else if (up && currentSelectedIndex > 0)
            mainMenuModel.MenuButtons[currentSelectedIndex - 1].Select();
    }
}
