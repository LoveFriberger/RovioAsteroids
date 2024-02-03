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

    [SerializeField]
    PlayerObject playerPrefab = null;

    public override void InstallBindings()
    {
        Container.BindInstance(startSceneKey).WhenInjectedInto<PreLoaderScene>();

        Container.BindInstance(new PointsController(highScorePlayerPrefsKey));
        Container.BindInstance(new GameController());
        Container.BindInstance(new SceneLoadingController());

        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();
    }
}
