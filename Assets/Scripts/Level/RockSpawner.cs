using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class RockSpawner : Spawner
{
    public class Settings
    {
        public int startRocks =0;
        public float timeBetweenRockSpawns = 0;
        public Transform rocksParent = null;
        public BoxCollider2D exitLevelCollider = null;
    }

    [Inject]
    Settings settings = null;
    [Inject]
    public Rock.Factory rockFactory = null;
    [SerializeField]
    public AssetReferenceGameObject rockPrefabReference = null;

    float lastRockSpawnTime = 0;
    AsyncOperationHandle<GameObject> LoadedRockHandle;

    IEnumerator Start()
    {
        lastRockSpawnTime = Time.time;
        LoadedRockHandle = new();

        yield return LoadAssetAsync(rockPrefabReference, (loadedHandle) => { LoadedRockHandle = loadedHandle; });

        InstantiateStartRocks();
    }

    void Update()
    {
        if (lastRockSpawnTime + settings.timeBetweenRockSpawns < Time.time)
            InstantiateRock();
    }

    protected override void ResetSpawns()
    {
        for (int i = settings.rocksParent.childCount - 1; i >= 0; i--)
        {
            Destroy(settings.rocksParent.GetChild(i).gameObject);
        }
        InstantiateStartRocks();
    }

    void InstantiateStartRocks()
    {
        for (int i = 0; i < settings.startRocks; i++)
            InstantiateRock();
    }

    void InstantiateRock()
    {
        var rockClone = rockFactory.Create();
        rockClone.transform.SetParent(settings.rocksParent);
        SetRockStartingConditions(rockClone);
        lastRockSpawnTime = Time.time;
    }

    void SetRockStartingConditions(Rock rockClone)
    {
        GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation);
        rockClone.transform.position = position;
        rockClone.transform.rotation = rotation;
    }

    void GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation)
    {
        var randomPointInLevel = RandomPointInLevel();
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        var randomRotationVector = (Vector2)(randomRotation * Vector2.up);
        var startDistance = settings.exitLevelCollider.bounds.size.magnitude * 1.2f;

        position = randomPointInLevel + randomRotationVector * startDistance;
        rotation = randomRotation * Quaternion.Euler(0, 0, 180);
    }

    Vector2 RandomPointInLevel()
    {
        var levelBounds = settings.exitLevelCollider.bounds;
        return new Vector2(Random.Range(levelBounds.min.x, levelBounds.max.x), Random.Range(levelBounds.min.y, levelBounds.max.y));
    }
}
