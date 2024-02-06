using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel
{
    readonly List<MenuButton> menuButtons = null;

    public MainMenuModel(List<MenuButton> menuButtons)
    {
        this.menuButtons = menuButtons;
    }

    public List<MenuButton> MenuButtons { get { return menuButtons; } }

    public int SelectedButtonIndex { get { return menuButtons.IndexOf(MenuButton.Selected); } }

}
