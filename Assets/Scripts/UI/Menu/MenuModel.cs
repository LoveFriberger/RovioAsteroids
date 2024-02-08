using System.Collections.Generic;

public class MenuModel
{
    readonly List<MenuButton> menuButtons = new();

    public MenuModel(List<MenuButton> menuButtons)
    {
        this.menuButtons = menuButtons;
    }

    public List<MenuButton> MenuButtons { get { return menuButtons; } }

    public int SelectedButtonIndex { get { return menuButtons.IndexOf(MenuButton.Selected); } }

}
