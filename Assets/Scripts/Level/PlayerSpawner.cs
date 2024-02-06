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
    readonly GameManagerController gameController = null;
    readonly Transform playerStart = null;

    public PlayerSpawner(AssetReferenceSpawner spawner, Settings settings, GameManagerController gameController, Transform playerStart)
    {
        this.spawner = spawner;
        this.settings = settings;
        this.gameController = gameController;
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
        public AssetReferenceGameObject playerPrefabReference = null;
    }
}
