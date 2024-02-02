using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    string highScorePlayerPrefsKey = "HighScore";

    public override void InstallBindings()
    {

        Container.BindInstance(new PointsController(highScorePlayerPrefsKey));
        Container.BindInstance(new GameController());
        Container.BindInstance(new SceneLoadingController());
    }
}
