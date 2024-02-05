using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<PointsController>().AsSingle();
        Container.Bind<GameController>().AsSingle();
        Container.Bind<SceneLoadingController>().AsSingle();


        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.BindInterfacesTo<InputController>().AsSingle();
    }


}
