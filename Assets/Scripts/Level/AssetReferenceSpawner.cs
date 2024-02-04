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

    bool IsLoaded(AssetReferenceGameObject asset)
    {
        return levelModel.LoadedAssets.ContainsKey(asset.AssetGUID);
    }

    public async Task Spawn(AssetReferenceGameObject asset, Transform parent = null, Action<GameObject> onSpawnedObject = null)
    {
        if (!IsLoaded(asset))
            await Load(asset);
        
        levelModel.AssetReferenceSpawner.Spawn(levelModel.LoadedAssets[asset.AssetGUID], parent, onSpawnedObject);
    }

    public async Task Spawn(AssetReferenceGameObject asset, Vector2 startPosition, Quaternion startRotation, Transform parent = null, Action<GameObject> onSpawnedObject = null)
    {
        if (!IsLoaded(asset))
            await Load(asset);

        levelModel.AssetReferenceSpawner.Spawn(levelModel.LoadedAssets[asset.AssetGUID], startPosition, startRotation, parent, onSpawnedObject);
    }

    async Task Load(AssetReferenceGameObject asset)
    {
        asset.LoadAssetAsync();

        await asset.OperationHandle.Task;

        if (asset.OperationHandle.Status == AsyncOperationStatus.Failed)
            Debug.LogError(asset.RuntimeKey + " failed to load!");
        else if (asset.OperationHandle.Status == AsyncOperationStatus.Succeeded)
            levelModel.LoadedAssets.Add(asset.AssetGUID, (GameObject)asset.OperationHandle.Result);
    }
}
