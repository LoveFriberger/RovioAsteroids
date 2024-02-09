using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using TMPro;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    LevelObject levelObject;
    [SerializeField]
    LevelUIObject levelUIObject;
    [SerializeField]
    Settings settings;

    public override void InstallBindings()
    {
        Container.Bind<LevelModel>().AsSingle().WithArguments(settings.menuObject);

        Container.Bind<MenuModel>().AsTransient().WithArguments(settings.menuButtons);
        Container.BindInterfacesAndSelfTo<MenuController>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelUIMenuOpener>().AsSingle();

        Container.BindInstance(settings.exitLevelTriggerObject.GetComponent<BoxCollider2D>()).WithId("exitLevelCollider").AsSingle();
        Container.BindInterfacesAndSelfTo<ExitLevelTrigger>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<RockSpawner>().AsSingle();
        Container.Bind<IAssetReferenceSpawner>().To<AssetReferenceSpawner>().AsSingle();
        Container.BindInstance(settings.playerStart).WhenInjectedInto<PlayerSpawner>();
        Container.BindInstance(settings.assetReferenceSpawnerObject).AsSingle();

        Container.BindInstances(levelObject);
        Container.BindInstances(levelUIObject);
        Container.BindInstances(settings.exitLevelTriggerObject);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceSpawnerObject assetReferenceSpawnerObject;
        public Transform playerStart;
        public List<MenuButton> menuButtons;
        public GameObject menuObject;
        public ExitLevelTriggerObject exitLevelTriggerObject;
    }
}
