using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class PlayerSpawner : IInitializable
{
    readonly LevelModel levelModel = null;
    readonly Settings settings = null;

    public PlayerSpawner(LevelModel levelModel, Settings settings)
    {
        this.levelModel = levelModel;
        this.settings = settings;
    }

    public void Initialize()
    {
        levelModel.AssetReferenceSpawner.Spawn(settings.playerPrefabReference);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceGameObject playerPrefabReference = null;
    }
}
