using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using NUnit.Framework;
using System;
using TMPro;

[TestFixture]
public class LevelUI : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        List<MenuButton> menuButtons = new();
        menuButtons.Add(CreateMenuButton());
        menuButtons.Add(CreateMenuButton());

        var menuObject = new GameObject();

        var tmpObjectTitle = new GameObject().AddComponent<TextMeshProUGUI>();
        Container.BindInstance(tmpObjectTitle).WithId("Title");

        var tmpObjectScore = new GameObject().AddComponent<TextMeshProUGUI>();
        Container.BindInstance(tmpObjectScore).WithId("Score");

        Container.Bind<LevelModel>().AsSingle().WithArguments(menuButtons, menuObject, tmpObjectTitle, tmpObjectScore);
        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();

        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();
        Container.Bind<PointsModel>().AsSingle();
        Container.Bind<PointsView>().AsSingle();

        Container.Bind<LevelUIMenuOpener.Settings>().AsSingle();

        Container.Bind<LevelUIMenuOpener>().AsSingle();

        Container.Bind<LevelUIInputController>().AsSingle();

        Container.Inject(this);
    }

    MenuButton CreateMenuButton()
    {
        var gameObject = new GameObject();
        return gameObject.AddComponent<MenuButton>();
    }

    [Inject]
    LevelModel levelModel;
    [Inject]
    LevelUIInputController levelUIInputController;
    [Inject]
    InputModel inputModel;
    [Inject]
    LevelUIMenuOpener levelUIMenuOpener;
    [Inject]
    LevelUIMenuOpener.Settings levelUIMenuOpenerSettings;
    [Inject(Id = "Title")]
    TextMeshProUGUI tmpObjectTitle;
    [Inject(Id = "Score")]
    TextMeshProUGUI tmpObjectScore;

    [Test]
    public void StartOnFirstButton()
    {
        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.SelectedButtonIndex == 0);
    }

    [Test]
    public void MoveButton()
    {
        levelUIMenuOpener.OpenMenu(false);

        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.SelectedButtonIndex == 0);

        inputModel.upInputDown = true;
        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.SelectedButtonIndex == 0);
        inputModel.upInputDown = false;

        inputModel.downInputDown = true;
        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.SelectedButtonIndex == 1);
        inputModel.downInputDown = false;

        inputModel.upInputDown = true;
        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.SelectedButtonIndex == 0);
    }

    [Test]
    public void ToggleMenu()
    {
        levelUIMenuOpener.CloseMenu();

        inputModel.toggleMenuInputDown = true;
        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.MenuObjectActivated);

        levelUIInputController.Tick();
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
        Assert.IsTrue(tmpObjectTitle.text == levelUIMenuOpenerSettings.pausedString);
        Assert.IsTrue(tmpObjectScore.text == "0");

        levelUIMenuOpener.CloseMenu();
        Assert.IsTrue(Time.timeScale == 1);
        Assert.IsFalse(levelModel.MenuObjectActivated);

        levelUIMenuOpener.OpenMenu(true);
        Assert.IsTrue(levelModel.PlayerDied);
        Assert.IsFalse(tmpObjectTitle.text == levelUIMenuOpenerSettings.pausedString);
    }
}
