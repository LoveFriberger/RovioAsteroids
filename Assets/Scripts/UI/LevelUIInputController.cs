using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelUIInputController : ITickable
{
    readonly LevelModel levelModel = null;
    readonly InputView inputView = null;
    readonly LevelUIMenuOpener levelUIMenuOpener = null;

    public LevelUIInputController(LevelModel levelModel, InputView inputView, LevelUIMenuOpener levelUIMenuOpener)
    {
        this.levelModel = levelModel;
        this.inputView = inputView;
        this.levelUIMenuOpener = levelUIMenuOpener;
    }

    public void Tick()
    {
        if (inputView.ToggleMenuInputDown)
            ToggleMenu();

        if (!levelModel.MenuObjectActivated)
            return;

        if (inputView.UpInputDown)
            ChangeSelectedButton(true);
        if (inputView.DownInputDown)
            ChangeSelectedButton(false);
        if (inputView.ActionInputDown)
            MenuButton.Selected.Click();
    }

    void ChangeSelectedButton(bool up)
    {
        var currentSelectedIndex = levelModel.SelectedButtonIndex;
        if (currentSelectedIndex < 0)
            return;

        if (!up && currentSelectedIndex < levelModel.MenuButtons.Count - 1)
            levelModel.MenuButtons[currentSelectedIndex + 1].Select();

        else if (up && currentSelectedIndex > 0 )
            levelModel.MenuButtons[currentSelectedIndex - 1].Select();
    }

    public void ToggleMenu()
    {
        if (!levelModel.MenuObjectActivated)
            levelUIMenuOpener.OpenMenu(false);
        else if (levelModel.CanCloseMenuWithKey)
            levelUIMenuOpener.CloseMenu();
        
    }
}
