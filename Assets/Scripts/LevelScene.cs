using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelScene : MonoBehaviour
{
    [SerializeField]
    int startRocks = 3;
    [SerializeField]
    float startVelocity = 3;
    [SerializeField]
    ExitLevelCollider exitLevelCollider = null;
    [SerializeField]
    Transform playerStart = null;
    [SerializeField]
    Transform rocksParent = null;
    [SerializeField]
    MenuOpener menuOpener = null;
    [SerializeField]
    AssetReferenceGameObject playerPrefabReference = null;
    [SerializeField]
    AssetReferenceGameObject rockPrefabReference = null;

    AsyncOperationHandle<GameObject> LoadedPlayerHandle;
    AsyncOperationHandle<GameObject> LoadedRockHandle;

    float timeBetweenSpawns = 5;
    float lastRockSpawnTime = 0;

    IEnumerator Start()
    {
        Core.Get<GameManager>().PlayerKilled += OnPlayerKilled;

        lastRockSpawnTime = Time.time;

        LoadedPlayerHandle = new();
        LoadedRockHandle = new();

        yield return LoadAssetAsync(playerPrefabReference, (loadedHandle) => { LoadedPlayerHandle = loadedHandle; });
        yield return LoadAssetAsync(rockPrefabReference, (loadedHandle) => { LoadedRockHandle = loadedHandle; });
        

        InstantiatePlayer();
        for (int i = 0; i < startRocks; i++)
            InstantiateRock();
    }

    private void OnDisable()
    {
        Core.Get<GameManager>().PlayerKilled -= OnPlayerKilled;
    }

    void OnPlayerKilled()
    {
        menuOpener.OpenMenu(true);
    }

    public void ResetGame()
    {
        menuOpener.CloseMenu();
        var pointsManager = Core.Get<PointsManager>();
        pointsManager.Reset();
        for (int i = rocksParent.childCount -1 ; i >= 0; i--)
        {
            Destroy(rocksParent.GetChild(i).gameObject);
        }

        Destroy(playerStart.GetChild(0).gameObject);

        InstantiatePlayer();
        for (int i = 0; i < startRocks; i++)
            InstantiateRock();
    }

    public IEnumerator LoadAssetAsync(AssetReferenceGameObject assetReferenceGameObject, System.Action<AsyncOperationHandle<GameObject>> onAssetLoaded)
    {
        var asyncLoadHandle = assetReferenceGameObject.LoadAssetAsync();
        yield return asyncLoadHandle;
        if(asyncLoadHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(assetReferenceGameObject.RuntimeKey + " failed to load!");
        }
        else if (asyncLoadHandle.Status == AsyncOperationStatus.Succeeded)
        {
            onAssetLoaded?.Invoke(asyncLoadHandle);
        }
    }

    void InstantiatePlayer()
    {
        Instantiate(LoadedPlayerHandle.Result, playerStart.position, playerStart.rotation, playerStart);
    }

    void Update()
    {
        if (lastRockSpawnTime + timeBetweenSpawns < Time.time)
            InstantiateRock();
    }

    void InstantiateRock()
    {
        GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation);
        var rockClone = Instantiate(LoadedRockHandle.Result, position, rotation, rocksParent);
        rockClone.GetComponent<Rigidbody2D>().velocity = rockClone.transform.up * startVelocity;
        lastRockSpawnTime = Time.time;
    }

    void GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation)
    {
        var randomPointInLevel = RandomPointInLevel();
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        var randomRotationVector = (Vector2)(randomRotation * Vector2.up);
        var startDistance = exitLevelCollider.GetComponent<Collider2D>().bounds.size.magnitude * 1.2f;

        position = randomPointInLevel + randomRotationVector * startDistance;
        rotation = randomRotation * Quaternion.Euler(0, 0, 180);
    }

    Vector2 RandomPointInLevel()
    {
        var levelBounds = exitLevelCollider.GetComponent<Collider2D>().bounds;
        return new Vector2(Random.Range(levelBounds.min.x, levelBounds.max.x), Random.Range(levelBounds.min.y, levelBounds.max.y));
    }

    void OnDestroy()
    {
        playerPrefabReference.ReleaseAsset();
        rockPrefabReference.ReleaseAsset();
    }
}
