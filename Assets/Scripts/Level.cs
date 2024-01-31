using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Level : Scene
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
    AssetReferenceGameObject playerPrefabReference = null;
    [SerializeField]
    AssetReferenceGameObject rockPrefabReference = null;

    GameObject LoadedPlayerPrefab = null;
    GameObject LoadedRockPrefab = null;

    float timeBetweenSpawns = 5;
    float lastRockSpawnTime = 0;

    protected override void Setup()
    {
        StartCoroutine(CoSetup());
    }

    IEnumerator CoSetup()
    {
        yield return LoadAssetAsync(playerPrefabReference, (loadedObject) => { LoadedPlayerPrefab = loadedObject; });
        yield return LoadAssetAsync(rockPrefabReference, (loadedObject) => { LoadedRockPrefab = loadedObject; });

        InstantiatePlayer();
        for (int i = 0; i < startRocks; i++)
            InstantiateRock();
    }

    IEnumerator LoadAssetAsync(AssetReferenceGameObject assetReferenceGameObject, System.Action<GameObject> onAssetLoaded)
    {
        var asyncLoadHandle = assetReferenceGameObject.LoadAssetAsync();
        yield return asyncLoadHandle;
        if(asyncLoadHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Failed)
        {
            Debug.LogError(assetReferenceGameObject.RuntimeKey + " failed to load!");
        }
        else if (asyncLoadHandle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            onAssetLoaded?.Invoke(asyncLoadHandle.Result);
            Addressables.Release(asyncLoadHandle);
        }
    }

    void InstantiatePlayer()
    {
        Instantiate(LoadedPlayerPrefab, playerStart.position, playerStart.rotation);
    }

    void Update()
    {
        if (lastRockSpawnTime + timeBetweenSpawns < Time.time)
            InstantiateRock();
    }

    void InstantiateRock()
    {
        GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation);
        var rockClone = Instantiate(LoadedRockPrefab, position, rotation);
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
}
