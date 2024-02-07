using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuModel
{
    readonly List<MenuButton> menuButtons = null;

    public MenuModel(List<MenuButton> menuButtons)
    {
        this.menuButtons = menuButtons;
    }

    public List<MenuButton> MenuButtons { get { return menuButtons; } }

    public int SelectedButtonIndex { get { return menuButtons.IndexOf(MenuButton.Selected); } }

}
