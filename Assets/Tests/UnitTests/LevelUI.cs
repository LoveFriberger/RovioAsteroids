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
        menuObject.SetActive(false);

        var tmpObject = new GameObject().AddComponent<TextMeshProUGUI>();

        Container.Bind<LevelModel>().AsSingle().WithArguments(menuButtons, menuObject, tmpObject, tmpObject);
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

    [Test]
    public void StartOnFirstButton()
    {
        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.SelectedButtonIndex == 0);
    }

    [Test]
    public void MoveButton()
    {
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
        inputModel.toggleMenuInputDown = true;
        levelUIInputController.Tick();
        Assert.IsTrue(levelModel.MenuObjectActivated);

        levelUIInputController.Tick();
        inputModel.toggleMenuInputDown = true;
        Assert.IsFalse(levelModel.MenuObjectActivated);
    }
}
