using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using TMPro;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    LevelObject levelObject = null;
    [SerializeField]
    LevelUIObject levelUIObject = null;
    [SerializeField]
    Settings settings = null;

    public override void InstallBindings()
    {
        Container.Bind<LevelModel>().AsSingle().WithArguments(settings);

        Container.BindInterfacesAndSelfTo<LevelUIInputController>().AsSingle();
        Container.Bind<LevelUIMenuOpener>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<RockSpawner>().AsSingle();
        Container.Bind<AssetReferenceSpawner>().AsSingle();
        Container.BindInstance(settings.playerStart).WhenInjectedInto<PlayerSpawner>();

        Container.BindInstances(levelObject);
        Container.BindInstances(levelUIObject);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceSpawnerObject assetReferenceSpawner = null;
        public BoxCollider2D levelCollider = null;
        public Transform playerStart = null;
        public List<MenuButton> menuButtons = new();
        public GameObject menuObject = null;
        public TextMeshProUGUI menuTitle = null;
        public TextMeshProUGUI menuScore = null;
    }
}
