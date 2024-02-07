using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NUnit.Framework;
using System;
using TMPro;

[TestFixture]
public class Level : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        var colliderObject = new GameObject().AddComponent<BoxCollider2D>();
        Container.BindInstance(colliderObject).WithId("exitLevelCollider").AsSingle();

        List<MenuButton> menuButtons = new();
        menuButtons.Add(CreateMenuButton());
        menuButtons.Add(CreateMenuButton());

        var menuObject = new GameObject();
        Container.Bind<LevelModel>().AsSingle().WithArguments(menuObject);
        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.Bind<InputController>().AsSingle();
        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();

        Container.Bind<LevelUIMenuOpener>().AsSingle();
        Container.Bind<ExitLevelTrigger>().AsSingle();

        Container.Inject(this);
    }

    MenuButton CreateMenuButton()
    {
        var gameObject = new GameObject();
        return gameObject.AddComponent<MenuButton>();
    }

    [Inject(Id = "exitLevelCollider")]
    BoxCollider2D levelCollider;
    [Inject]
    ExitLevelTrigger exitLevelTrigger;
    [Inject]
    LevelModel levelModel;
    [Inject]
    LevelUIMenuOpener levelUIMenuOpener;
    [Inject]
    InputModel inputModel;

    [Test]
    public void SetLevelColliderToCameraSize()
    {
        var camera = new GameObject().AddComponent<Camera>();
        var cameraOrthographicWidth = camera.orthographicSize * camera.aspect;
        Assert.IsFalse(levelCollider.size == new Vector2(cameraOrthographicWidth, Camera.main.orthographicSize) * 2);

        exitLevelTrigger.Initialize();

        Assert.IsTrue(levelCollider.size == new Vector2(cameraOrthographicWidth, Camera.main.orthographicSize) * 2);
    }

    [Test]
    public void ToggleMenu()
    {
        levelUIMenuOpener.CloseMenu();

        inputModel.toggleMenuInputDown = true;
        levelUIMenuOpener.Tick();
        Assert.IsTrue(levelModel.MenuObjectActivated);

        levelUIMenuOpener.Tick();
        inputModel.toggleMenuInputDown = true;
        Assert.IsFalse(levelModel.MenuObjectActivated);
    }

    [Test]
    public void MenuOpener()
    {
        levelUIMenuOpener.OpenMenu(false);
        Assert.IsTrue(Time.timeScale == 0);
        Assert.IsFalse(levelModel.PlayerDied);
        Assert.IsTrue(levelModel.MenuObjectActivated);

        levelUIMenuOpener.CloseMenu();
        Assert.IsTrue(Time.timeScale == 1);
        Assert.IsFalse(levelModel.MenuObjectActivated);

        levelUIMenuOpener.OpenMenu(true);
        Assert.IsTrue(levelModel.PlayerDied);
    }
}
