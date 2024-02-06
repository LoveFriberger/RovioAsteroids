using Zenject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;


[TestFixture]
public class Points : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        Container.Bind<PointsModel>().AsSingle();
        Container.Bind<PointsView>().AsSingle();
        Container.Bind<PointsController>().AsSingle();

        Container.Inject(this);
    }

    [Inject]
    PointsModel pointsModel;
    [Inject]
    PointsView pointsView;
    [Inject]
    PointsController pointsController;


    [Test]
    public void TestAddValues()
    {
        var oldPoints = pointsView.CurrentPoints;
        pointsController.AddPoints(1);
        Assert.That(oldPoints + 1 == pointsView.CurrentPoints);
    }

    [Test]
    public void TestResetHighScore()
    {
        pointsController.AddPoints(1);
        Assert.That(pointsView.HighScore > 0);
        pointsController.Reset();
        Assert.That(pointsView.HighScore == 0);
        Assert.That(!pointsView.NewHighScore);
    }

    [Test]
    public void TestNewHighScore()
    {
        pointsController.Reset();
        Assert.That(!pointsView.NewHighScore);
        pointsController.AddPoints(1);
        Assert.That(pointsView.NewHighScore);
        Assert.That(pointsView.HighScore == 1);
    }

    [Test]
    public void TestAddOnScoreUpdatedAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        pointsController.AddOnScoreUpdatedAction(action);
        Assert.That(!testValue);
        pointsModel.onScoreUpdated.Invoke();
        Assert.That(testValue);
    }

    [Test]
    public void TestRemoveOnScoreUpdatedAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        pointsController.AddOnScoreUpdatedAction(action);
        pointsController.RemoveOnScoreUpdatedAction(action);

        Assert.That(testValue == false);
        Assert.That(pointsModel.onScoreUpdated == null);
    }
}