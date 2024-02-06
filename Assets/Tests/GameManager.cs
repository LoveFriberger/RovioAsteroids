using Zenject;
using NUnit.Framework;
using System;
using UnityEngine;

[TestFixture]
public class GameManager : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();

        Container.Inject(this);
    }

    [Inject]
    GameManagerModel gameManagerModel;
    [Inject]
    GameManagerController gameManagerController;

    [Test]
    public void InvokePlayerKilledAction()
    {
        gameManagerController.InvokePlayerKilledAction();
    }

    [Test]
    public void AddPlayerKilledAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        gameManagerController.AddPlayerKilledAction(action);
        Assert.That(!testValue);
        gameManagerController.InvokePlayerKilledAction();
        Assert.That(testValue);
    }

    [Test]
    public void RemovePlayerKilledAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        gameManagerController.AddPlayerKilledAction(action);
        gameManagerController.RemovePlayerKilledAction(action);

        Assert.That(testValue == false);
        Assert.That(gameManagerModel.onPlayerKilled == null);
    }

    [Test]
    public void InvokeResetGameAction()
    {
        gameManagerController.InvokeResetGameAction();
    }

    [Test]
    public void AddResetGameAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        gameManagerController.AddResetGameAction(action);
        Assert.That(!testValue);
        gameManagerController.InvokeResetGameAction();
        Assert.That(testValue);
    }

    [Test]
    public void RemoveResetGameAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        gameManagerController.AddResetGameAction(action);
        gameManagerController.RemoveResetGameAction(action);

        Assert.That(testValue == false);
        Assert.That(gameManagerModel.onResetGame == null);
    }

    [Test]
    public void Pause()
    {
        gameManagerController.SetPause(true);
        Assert.That(Time.timeScale == 0);
        gameManagerController.SetPause(false);
        Assert.That(Time.timeScale == 1);
    }
}
