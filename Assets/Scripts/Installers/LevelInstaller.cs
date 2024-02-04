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
    AssetReferenceSpawnerObject assetReferenceSpawner = null;
    [SerializeField]
    BoxCollider2D levelCollider = null;

    public override void InstallBindings()
    {
        Container.Bind<LevelModel>().AsSingle().WithArguments(assetReferenceSpawner, levelCollider);

        Container.BindInterfacesTo<PlayerSpawner>().AsSingle();
        Container.BindInterfacesTo<RockSpawner>().AsSingle();
        Container.Bind<AssetReferenceSpawner>().AsSingle();

        Container.BindInstances(levelObject);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceSpawnerObject assetReferenceSpawner = null;
        public BoxCollider2D levelCollider = null;
    }
}
