using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class RockSpawner : IInitializable, ITickable
{
    readonly LevelModel levelModel = null;
    readonly Settings settings = null;

    public RockSpawner(LevelModel levelModel, Settings settings)
    {
        this.levelModel = levelModel;
        this.settings = settings;
    }

    public void Initialize()
    {
        levelModel.LastRockSpawnTime = Time.time;
        InstantiateStartRocks();
    }

    public void Tick()
    {
        if (levelModel.LastRockSpawnTime + settings.timeBetweenRockSpawns < Time.time)
            InstantiateRock();
    }

    void InstantiateStartRocks()
    {
        for (int i = 0; i < settings.startRocks; i++)
            InstantiateRock();
    }

    void InstantiateRock()
    {
        GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation);
        levelModel.AssetReferenceSpawner.Spawn(settings.rockPrefabReference, position, rotation);
        levelModel.LastRockSpawnTime = Time.time;
    }

    void GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation)
    {
        var randomPointInLevel = RandomPointInLevel();
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        var randomRotationVector = (Vector2)(randomRotation * Vector2.up);
        var startDistance = levelModel.ExitLevelCollider.bounds.size.magnitude * 1.2f;

        position = randomPointInLevel + randomRotationVector * startDistance;
        rotation = randomRotation * Quaternion.Euler(0, 0, 180);
    }

    Vector2 RandomPointInLevel()
    {
        var levelBounds = levelModel.ExitLevelCollider.bounds;
        return new Vector2(Random.Range(levelBounds.min.x, levelBounds.max.x), Random.Range(levelBounds.min.y, levelBounds.max.y));
    }

    [System.Serializable]
    public class Settings
    {
        public AssetReferenceGameObject rockPrefabReference = null;
        public int startRocks = 0;
        public float timeBetweenRockSpawns = 0;
    }
}
