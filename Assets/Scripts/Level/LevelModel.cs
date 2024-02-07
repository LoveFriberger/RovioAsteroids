using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class LevelModel
{
    readonly GameObject menuObject = null;

    public LevelModel(GameObject menuObject)
    {
        this.menuObject = menuObject;
    }

    public bool MenuObjectActivated { get { return menuObject.activeSelf; } set { menuObject.SetActive(value); } }

    public bool CanCloseMenuWithKey { get { return !PlayerDied; } }

    public bool PlayerDied { get; set; }
}
