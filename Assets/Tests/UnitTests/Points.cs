using Zenject;
using NUnit.Framework;
using System;

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
    public void AddValues()
    {
        var oldPoints = pointsView.CurrentPoints;
        pointsController.AddPoints(1);
        Assert.That(oldPoints + 1 == pointsView.CurrentPoints);
    }

    [Test]
    public void ResetCurrentPoints()
    {
        pointsController.AddPoints(1);
        Assert.That(pointsView.HighScore > 0);
        Assert.That(pointsView.CurrentPoints > 0);
        pointsController.ResetGamePoints();
        Assert.That(pointsView.HighScore > 0);
        Assert.That(pointsView.CurrentPoints == 0);
        Assert.That(!pointsView.NewHighScore);
    }

    [Test]
    public void NewHighScore()
    {
        pointsController.ResetHighScore();
        pointsController.ResetGamePoints();
        Assert.That(!pointsView.NewHighScore);
        pointsController.AddPoints(1);
        Assert.That(pointsView.NewHighScore);
        Assert.That(pointsView.HighScore == 1);
    }

    [Test]
    public void AddScoreUpdatedAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        pointsController.AddScoreUpdatedAction(action);
        Assert.That(!testValue);
        pointsModel.onScoreUpdated.Invoke();
        Assert.That(testValue);
    }

    [Test]
    public void RemoveScoreUpdatedAction()
    {
        var testValue = false;
        Action action = () => testValue = true;
        pointsController.AddScoreUpdatedAction(action);
        pointsController.RemoveScoreUpdatedAction(action);

        Assert.That(testValue == false);
        Assert.That(pointsModel.onScoreUpdated == null);
    }
}