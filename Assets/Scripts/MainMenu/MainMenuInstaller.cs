using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField]
    Settings settings = null;

    public override void InstallBindings()
    {
        Container.Bind<MenuModel>().AsTransient().WithArguments(settings.menuButtons);

        Container.BindInterfacesTo<MenuController>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public List<MenuButton> menuButtons = null;
    }
}
