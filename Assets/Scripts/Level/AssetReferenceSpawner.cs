using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetReferenceSpawner : MonoBehaviour
{
    AsyncOperationHandle<GameObject> asyncLoadHandle;
    
    public void Spawn(AssetReferenceGameObject assetReferenceGameObject, Transform parent, Action<GameObject> onSpawnedObject)
    {
        StartCoroutine(CoSpawn(assetReferenceGameObject, parent, onSpawnedObject));
    }

    public IEnumerator CoSpawn(AssetReferenceGameObject assetReferenceGameObject, Transform parent, Action<GameObject> onSpawnedObject)
    {
        if(!asyncLoadHandle.IsValid())
            asyncLoadHandle = assetReferenceGameObject.LoadAssetAsync();
        yield return asyncLoadHandle;
        if (asyncLoadHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(assetReferenceGameObject.RuntimeKey + " failed to load!");
        }
        else if (asyncLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            var spawnedObject = Instantiate(asyncLoadHandle.Result, transform.position, transform.rotation, parent);
            onSpawnedObject?.Invoke(spawnedObject);
            //Take care of the handle
        }
    }
}
