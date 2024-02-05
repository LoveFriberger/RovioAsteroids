using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel
{
    readonly List<MenuButton> menuButtons = null;

    public MainMenuModel(MainMenuInstaller.Settings settings)
    {
        this.menuButtons = settings.menuButtons;
    }

    public List<MenuButton> MenuButtons { get { return menuButtons; } }

    public int SelectedButtonIndex { get { return menuButtons.IndexOf(MenuButton.Selected); } }

}
