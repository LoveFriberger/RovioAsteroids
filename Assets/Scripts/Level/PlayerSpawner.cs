using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class PlayerSpawner : IInitializable
{
    readonly AssetReferenceSpawner spawner = null;
    readonly Settings settings = null;

    public PlayerSpawner(AssetReferenceSpawner spawner, Settings settings)
    {
        this.spawner = spawner;
        this.settings = settings;
    }

    public async void Initialize()
    {
        await spawner.Spawn(settings.playerPrefabReference);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceGameObject playerPrefabReference = null;
    }
}
