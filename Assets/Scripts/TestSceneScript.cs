using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TestSceneScript : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab = null;
    [SerializeField]
    AssetReferenceGameObject assetReferenceGameObject = null;

    IEnumerator Start()
    {
        var asyncLoadHandle = assetReferenceGameObject.LoadAssetAsync();
        yield return asyncLoadHandle;
        if (asyncLoadHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(assetReferenceGameObject.RuntimeKey + " failed to load!");
        }
        else if (asyncLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(asyncLoadHandle.Result);
        }
    }

}
