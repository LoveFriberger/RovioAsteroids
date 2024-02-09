using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class PlayerSpawner : IInitializable
{
    readonly IAssetReferenceSpawner spawner;
    readonly Settings settings;
    readonly Transform playerStart;

    public PlayerSpawner(
        IAssetReferenceSpawner spawner, 
        Settings settings,
        Transform playerStart)
    {
        this.spawner = spawner;
        this.settings = settings;
        this.playerStart = playerStart;
    }

    public void Initialize()
    {
        SpawnPlayer();
    }

    public async void SpawnPlayer()
    {
        var position = playerStart.position;
        var rotation = playerStart.rotation;
        await spawner.Spawn(settings.playerPrefabReference, position, rotation);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceGameObject playerPrefabReference;
    }
}
