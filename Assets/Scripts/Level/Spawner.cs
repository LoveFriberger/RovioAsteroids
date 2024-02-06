using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public abstract class Spawner : MonoBehaviour
{
    [Inject]
    protected GameManagerController gameController = null;

    void OnEnable()
    {
        gameController.AddResetGameAction(ResetSpawns);
    }

    void OnDisable()
    {
        gameController.AddResetGameAction(ResetSpawns);
    }

    public IEnumerator LoadAssetAsync(AssetReferenceGameObject assetReferenceGameObject, System.Action<AsyncOperationHandle<GameObject>> onAssetLoaded)
    {
        
        var asyncLoadHandle = assetReferenceGameObject.LoadAssetAsync();
        yield return asyncLoadHandle;
        if (asyncLoadHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(assetReferenceGameObject.RuntimeKey + " failed to load!");
        }
        else if (asyncLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            onAssetLoaded?.Invoke(asyncLoadHandle);
        }
    }

    protected abstract void ResetSpawns();
}
