using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetReferenceSpawner : MonoBehaviour
{
    AsyncOperationHandle<GameObject> asyncLoadHandle;
    
    public void Spawn(AssetReferenceGameObject assetReferenceGameObject, Transform parent = null, Action<GameObject> onSpawnedObject = null)
    {
        StartCoroutine(CoSpawn(assetReferenceGameObject, transform.position, transform.rotation, parent, onSpawnedObject));
    }

    public void Spawn(AssetReferenceGameObject assetReferenceGameObject, Vector2 startPosition, Quaternion startRotation, Transform parent = null, Action<GameObject> onSpawnedObject = null)
    {
        StartCoroutine(CoSpawn(assetReferenceGameObject, startPosition, startRotation, parent, onSpawnedObject));
    }

    IEnumerator CoSpawn(AssetReferenceGameObject assetReferenceGameObject, Vector2 startPosition, Quaternion startRotation, Transform parent = null, Action<GameObject> onSpawnedObject = null)
    {
        if(!asyncLoadHandle.IsValid())
            asyncLoadHandle = assetReferenceGameObject.LoadAssetAsync();
        while (!asyncLoadHandle.IsDone)
            yield return null;
        if (asyncLoadHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(assetReferenceGameObject.RuntimeKey + " failed to load!");
        }
        else if (asyncLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            var spawnedObject = Instantiate(asyncLoadHandle.Result, startPosition, startRotation, parent);
            onSpawnedObject?.Invoke(spawnedObject);
            //Take care of the handle
        }
    }
}
