using System.Collections;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    LevelObject levelObject = null;
    [SerializeField]
    Settings settings = null;

    public override void InstallBindings()
    {
        Container.Bind<LevelModel>().AsSingle().WithArguments(settings.assetReferenceSpawner, settings.levelCollider);

        Container.BindInterfacesAndSelfTo<PlayerSpawner>().AsSingle();
        Container.BindInterfacesAndSelfTo<RockSpawner>().AsSingle();
        Container.Bind<AssetReferenceSpawner>().AsSingle();
        Container.BindInstance(settings.playerStart).WhenInjectedInto<PlayerSpawner>();
        Container.BindInstances(levelObject);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceSpawnerObject assetReferenceSpawner = null;
        public BoxCollider2D levelCollider = null;
        public Transform playerStart = null;
    }
}
