using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    string startSceneKey = "";
    [SerializeField]
    string highScorePlayerPrefsKey = "";

    public override void InstallBindings()
    {
        Container.BindInstance(startSceneKey).WhenInjectedInto<PreLoaderScene>();

        Container.BindInstance(new PointsController(highScorePlayerPrefsKey));
        Container.BindInstance(new GameController());
        Container.BindInstance(new SceneLoadingController());
    }
}
