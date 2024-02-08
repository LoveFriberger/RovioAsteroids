using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using System.Threading.Tasks;

public class AssetReferenceSpawner
{
    readonly AssetReferenceSpawnerObject assetReferenceSpawnerObject;

    public AssetReferenceSpawner(AssetReferenceSpawnerObject assetReferenceSpawnerObject)
    {
        this.assetReferenceSpawnerObject = assetReferenceSpawnerObject;
    }

    public async Task Spawn(AssetReferenceGameObject asset, Vector2 startPosition, Quaternion startRotation, Action<GameObject> onSpawnedObject = null)
    {
        if (!asset.IsValid())
            await Load(asset);

        Debug.Log(string.Format("Starting spawning of {0}", asset.AssetGUID));
        assetReferenceSpawnerObject.Spawn((GameObject)asset.OperationHandle.Result, startPosition, startRotation, onSpawnedObject);
    }

    async Task Load(AssetReferenceGameObject asset)
    {
        Debug.Log(string.Format("Starting loading of {0}", asset.AssetGUID));
        asset.LoadAssetAsync();

        await asset.OperationHandle.Task;

        if (asset.OperationHandle.Status == AsyncOperationStatus.Failed)
            Debug.LogError(string.Format("{0} failed to load!", asset.AssetGUID));
        else
            Debug.Log(string.Format("Loaded {0}", asset.AssetGUID));

    }
}
