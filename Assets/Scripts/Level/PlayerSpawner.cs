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
    [SerializeField]
    AssetReferenceGameObject playerPrefabReference = null;
    [Inject]
    Player.Factory playerFactory = null;

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
        var playerClone = playerFactory.Create(); 
        playerClone.transform.position = playerStart.position;
        playerClone.transform.rotation = playerStart.rotation;
        playerClone.transform.SetParent(playerStart);
        //Instantiate(LoadedPlayerHandle.Result, playerStart.position, playerStart.rotation, playerStart);
    }

    void OnDestroy()
    {
        playerPrefabReference?.ReleaseAsset();
    }
}
