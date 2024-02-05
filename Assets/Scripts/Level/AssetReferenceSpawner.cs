using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using System.Threading.Tasks;

public class AssetReferenceSpawner
{
    readonly LevelModel levelModel = null;

    public AssetReferenceSpawner(LevelModel levelModel)
    {
        this.levelModel = levelModel;
    }

    public async Task Spawn(AssetReferenceGameObject asset, Vector2 startPosition, Quaternion startRotation, Action<GameObject> onSpawnedObject = null)
    {
        if (!asset.IsValid())
            await Load(asset);

        levelModel.AssetReferenceSpawner.Spawn((GameObject)asset.OperationHandle.Result, startPosition, startRotation, onSpawnedObject);
    }

    async Task Load(AssetReferenceGameObject asset)
    {
        asset.LoadAssetAsync();

        await asset.OperationHandle.Task;

        if (asset.OperationHandle.Status == AsyncOperationStatus.Failed)
            Debug.LogError(asset.RuntimeKey + " failed to load!");
    }
}
