using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

#if UNITY_EDITOR
public class TestAssetReferenceSpawner : IAssetReferenceSpawner
{

    readonly AssetReferenceSpawnerObject assetReferenceSpawnerObject;

    public TestAssetReferenceSpawner(AssetReferenceSpawnerObject assetReferenceSpawnerObject)
    {
        this.assetReferenceSpawnerObject = assetReferenceSpawnerObject;
    }

    public async Task Spawn(AssetReferenceGameObject asset, Vector2 startPosition, Quaternion startRotation, Action<GameObject> onSpawnedObject = null)
    {
        assetReferenceSpawnerObject.Spawn(asset.editorAsset, startPosition, startRotation, onSpawnedObject);
        await Task.CompletedTask;
    }

}
#endif