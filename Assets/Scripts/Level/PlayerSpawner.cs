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
    readonly GameController gameController = null;
    readonly Transform playerStart = null;

    public PlayerSpawner(AssetReferenceSpawner spawner, Settings settings, GameController gameController, Transform playerStart)
    {
        this.spawner = spawner;
        this.settings = settings;
        this.gameController = gameController;
        this.playerStart = playerStart;
    }

    public void Initialize()
    {
        gameController.AddResetGameAction(SpawnPlayer);
        SpawnPlayer();
    }

    public async void SpawnPlayer()
    {
        await spawner.Spawn(settings.playerPrefabReference, playerStart.position, playerStart.rotation);
    }
    public void RemoveOnResetAction()
    {
        gameController.RemoveResetGameAction(SpawnPlayer);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceGameObject playerPrefabReference = null;
    }
}
