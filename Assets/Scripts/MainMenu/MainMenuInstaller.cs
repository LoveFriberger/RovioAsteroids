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
        Container.Bind<MainMenuModel>().AsSingle().WithArguments(settings.menuButtons);

        Container.BindInterfacesTo<MainMenuController>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public List<MenuButton> menuButtons = null;
    }
}
