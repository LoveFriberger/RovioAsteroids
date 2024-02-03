using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class PlayerSpawner : Spawner
{
    public class Settings
    {
        public Transform playerStart = null;
        
        public AssetReferenceGameObject playerPrefabReference = null;
    }
    [Inject]
    Settings settings = null;

    AsyncOperationHandle<GameObject> LoadedPlayerHandle;


    IEnumerator Start()
    {
        LoadedPlayerHandle = new();
        yield return LoadAssetAsync(settings.playerPrefabReference, (loadedHandle) => { LoadedPlayerHandle = loadedHandle; });
        InstantiatePlayer();
    }

    protected override void ResetSpawns()
    {
        Destroy(settings.playerStart.GetChild(0).gameObject);
        InstantiatePlayer();
    }

    void InstantiatePlayer()
    {
        Instantiate(LoadedPlayerHandle.Result, settings.playerStart.position, settings.playerStart.rotation, settings.playerStart);
    }

    void OnDestroy()
    {
    }
}
