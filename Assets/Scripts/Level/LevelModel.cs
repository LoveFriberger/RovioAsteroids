using UnityEngine;

public class LevelModel
{
    readonly GameObject menuObject;

    public LevelModel(GameObject menuObject)
    {
        this.menuObject = menuObject;
    }

    public bool MenuObjectActivated { get { return menuObject.activeSelf; } set { menuObject.SetActive(value); } }

    public bool CanCloseMenuWithKey { get { return !PlayerDied; } }

    public bool PlayerDied { get; set; }
}
