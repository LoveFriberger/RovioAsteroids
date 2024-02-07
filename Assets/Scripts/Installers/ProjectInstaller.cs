using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PointsModel>().AsSingle();
        Container.Bind<PointsView>().AsSingle();
        Container.Bind<PointsController>().AsSingle();

        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();

        Container.Bind<SceneLoadingModel>().AsSingle();
        Container.Bind<SceneLoadingController>().AsSingle();

        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.BindInterfacesAndSelfTo<InputController>().AsSingle();
    }
}
