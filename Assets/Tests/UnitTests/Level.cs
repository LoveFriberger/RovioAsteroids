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

        Container.Bind<ExitLevelTrigger>().AsSingle();

        Container.Inject(this);
    }

    [Inject(Id = "exitLevelCollider")]
    BoxCollider2D levelCollider;
    [Inject]
    ExitLevelTrigger exitLevelTrigger;

    [Test]
    public void SetLevelColliderToCameraSize()
    {
        var camera = new GameObject().AddComponent<Camera>();
        var cameraOrthographicWidth = camera.orthographicSize * camera.aspect;
        Assert.IsFalse(levelCollider.size == new Vector2(cameraOrthographicWidth, Camera.main.orthographicSize) * 2);

        exitLevelTrigger.Initialize();

        Assert.IsTrue(levelCollider.size == new Vector2(cameraOrthographicWidth, Camera.main.orthographicSize) * 2);
    }
}
