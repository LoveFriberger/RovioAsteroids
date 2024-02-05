using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class LevelModel
{
    readonly AssetReferenceSpawnerObject assetReferenceSpawner = null;
    readonly BoxCollider2D levelCollider = null;
    readonly List<MenuButton> menuButtons = null;
    readonly GameObject menuObject = null;
    readonly TextMeshProUGUI menuTitle = null;
    readonly TextMeshProUGUI menuScore = null;

    public LevelModel(LevelInstaller.Settings settings)
    {
        this.assetReferenceSpawner = settings.assetReferenceSpawner;
        this.levelCollider = settings.levelCollider;
        this.menuButtons = settings.menuButtons;
        this.menuObject = settings.menuObject;
        this.menuTitle = settings.menuTitle;
        this.menuScore = settings.menuScore;
    }

    public AssetReferenceSpawnerObject AssetReferenceSpawner { get { return assetReferenceSpawner; } }

    public float LastRockSpawnTime { get; set; }

    public BoxCollider2D ExitLevelCollider { get { return levelCollider; }}

    public List<MenuButton> MenuButtons { get { return menuButtons; } }

    public int SelectedButtonIndex { get { return menuButtons.IndexOf(MenuButton.Selected); } }

    public bool MenuObjectActivated { get { return menuObject.activeSelf; } set { menuObject.SetActive(value); } }

    public bool CanCloseMenuWithKey { get { return !PlayerDied; } }

    public bool PlayerDied { get; set; }

    public string MenuDisplayedTitle { set { menuTitle.text = value; } }

    public string MenuDisplayedScore { set { menuScore.text = value; } }
}
