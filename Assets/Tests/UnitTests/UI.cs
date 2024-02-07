using Zenject;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[TestFixture]
public class UI : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        List<MenuButton> menuButtons = new();
        menuButtons.Add(CreateMenuButton());
        menuButtons.Add(CreateMenuButton());

        Container.Bind<MenuModel>().AsSingle().WithArguments(menuButtons);
        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.Bind<MenuController>().AsSingle();

        Container.Inject(this);
    }

    MenuButton CreateMenuButton()
    {
        var gameObject = new GameObject();
        return gameObject.AddComponent<MenuButton>();
    }

    [Inject]
    MenuModel mainMenuModel = null;
    [Inject]
    InputModel inputModel = null;
    [Inject]
    MenuController mainMenuController = null;

    [Test]
    public void StartMenuOnFirstButton()
    {
        mainMenuController.Tick();
        Assert.IsTrue(mainMenuModel.SelectedButtonIndex == 0);
    }

    [Test]
    public void MoveMenuButton()
    {
        mainMenuController.Tick();
        Assert.IsTrue(mainMenuModel.SelectedButtonIndex == 0);

        inputModel.upInputDown = true;
        mainMenuController.Tick();
        Assert.IsTrue(mainMenuModel.SelectedButtonIndex == 0);
        inputModel.upInputDown = false;

        inputModel.downInputDown = true;
        mainMenuController.Tick();
        Assert.IsTrue(mainMenuModel.SelectedButtonIndex == 1);
        inputModel.downInputDown = false;

        inputModel.upInputDown = true;
        mainMenuController.Tick();
        Assert.IsTrue(mainMenuModel.SelectedButtonIndex == 0);
    }
}
