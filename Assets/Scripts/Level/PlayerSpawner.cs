using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class PlayerSpawner : Spawner
{
    [Inject]
    Transform playerStart = null;
    [Inject]
    AssetReferenceGameObject playerPrefabReference = null;
    [SerializeField]
    GameObject playerPrefab = null;

    AsyncOperationHandle<GameObject> LoadedPlayerHandle;

    IEnumerator Start()
    {
        LoadedPlayerHandle = new();
        yield return LoadAssetAsync(playerPrefabReference, (loadedHandle) => { LoadedPlayerHandle = loadedHandle; });
        InstantiatePlayer();
    }

    protected override void ResetSpawns()
    {
        Destroy(playerStart.GetChild(0).gameObject);
        InstantiatePlayer();
    }

    void InstantiatePlayer()
    {
        Instantiate(LoadedPlayerHandle.Result, playerStart.position, playerStart.rotation, playerStart);
    }

    void OnDestroy()
    {
        playerPrefabReference?.ReleaseAsset();
    }
}
