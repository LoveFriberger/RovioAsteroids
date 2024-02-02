using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class PlayerSpawner : Spawner
{
    public class Settings
    {
        public Transform playerStart = null;
    }
    [Inject]
    Settings settings = null;
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
        Destroy(settings.playerStart.GetChild(0).gameObject);
        InstantiatePlayer();
    }

    void InstantiatePlayer()
    {
        var playerClone = playerFactory.Create(); 
        playerClone.transform.position = settings.playerStart.position;
        playerClone.transform.rotation = settings.playerStart.rotation;
        playerClone.transform.SetParent(settings.playerStart);
        //Instantiate(LoadedPlayerHandle.Result, playerStart.position, playerStart.rotation, playerStart);
    }

    void OnDestroy()
    {
        playerPrefabReference?.ReleaseAsset();
    }
}
