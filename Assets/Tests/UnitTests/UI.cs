using Zenject;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

[TestFixture]
public class UI : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        List<MenuButton> menuButtons = new();
        menuButtons.Add(CreateMenuButton());
        menuButtons.Add(CreateMenuButton());

        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();
        Container.Bind<MenuModel>().AsSingle().WithArguments(menuButtons);
        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.Bind<MenuController>().AsSingle();
        Container.Bind<PointsModel>().AsSingle();
        Container.Bind<PointsView>().AsSingle();
        Container.Bind<PointsController>().AsSingle();
        Container.Bind<GameMenuTitleText.Settings>().AsSingle();
        Container.Bind<LevelModel>().AsSingle().WithArguments(new GameObject());
        Container.Inject(this);
    }

    MenuButton CreateMenuButton()
    {
        var gameObject = new GameObject();
        return gameObject.AddComponent<MenuButton>();
    }

    [Inject]
    MenuModel mainMenuModel;
    [Inject]
    InputModel inputModel;
    [Inject]
    MenuController mainMenuController;
    [Inject]
    PointsController pointsController;
    [Inject]
    GameMenuTitleText.Settings gameMenuTitleTextSettings;
    [Inject]
    LevelModel levelModel;
    [Inject]
    PointsModel pointsModel;
    [Inject]
    PointsView pointsView;

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

    [Test]
    public void ScoreIndicator()
    {
        pointsController.AddPoints(3);
        pointsController.NewHighScore(4);

        var scoreIndicatorObject = new GameObject().AddComponent<ScoreIndicator>();
        var serializedScoreIndicator = new SerializedObject(scoreIndicatorObject);

        var scoreTMPObject = new GameObject().AddComponent<TextMeshProUGUI>();
        serializedScoreIndicator.FindProperty("scoreText").objectReferenceValue = scoreTMPObject;
        var scoreString = serializedScoreIndicator.FindProperty("scoreString").stringValue;
        serializedScoreIndicator.ApplyModifiedProperties();

        Container.Inject(scoreIndicatorObject);

        Assert.IsTrue(scoreTMPObject.text == string.Format(scoreString, 3, 4));
    }

    [Test]
    public void GameMenuTitleText()
    {
        var gameMenuTitleTextObject = new GameObject().AddComponent<GameMenuTitleText>();
        var serializedGameMenuTitleText = new SerializedObject(gameMenuTitleTextObject);

        var titleTMPObject = new GameObject().AddComponent<TextMeshProUGUI>();
        serializedGameMenuTitleText.FindProperty("titleText").objectReferenceValue = titleTMPObject;
        serializedGameMenuTitleText.ApplyModifiedProperties();

        Container.Inject(gameMenuTitleTextObject);

        Assert.IsTrue(titleTMPObject.text == gameMenuTitleTextSettings.pausedString);

        levelModel.PlayerDied = true;
        Container.Inject(gameMenuTitleTextObject);

        Assert.IsTrue(titleTMPObject.text == gameMenuTitleTextSettings.scoreString);

        pointsModel.newHighScore = true;
        Container.Inject(gameMenuTitleTextObject);

        Assert.IsTrue(titleTMPObject.text == gameMenuTitleTextSettings.newHighScoreString);
    }

    [Test]
    public void HighScoreIndicator()
    {
        var highScoreIndicatorObject = new GameObject().AddComponent<HighScoreIndicator>();
        var serializedHighScoreIndicator = new SerializedObject(highScoreIndicatorObject);

        var highScoreTMPObject = new GameObject().AddComponent<TextMeshProUGUI>();
        serializedHighScoreIndicator.FindProperty("highScoreText").objectReferenceValue = highScoreTMPObject;
        var highScoreString = serializedHighScoreIndicator.FindProperty("highScoreString").stringValue;
        serializedHighScoreIndicator.ApplyModifiedProperties();

        pointsController.AddPoints(4);
        Container.Inject(highScoreIndicatorObject);

        Assert.IsTrue(highScoreTMPObject.text == string.Format(highScoreString, pointsView.HighScore));
    }
}
