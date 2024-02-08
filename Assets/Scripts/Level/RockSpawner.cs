using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class RockSpawner : IInitializable, ITickable
{
    readonly AssetReferenceSpawner spawner;
    readonly Settings settings;
    readonly BoxCollider2D exitLevelBoxCollider;

    float lastRockSpawnTime = 0;

    public RockSpawner(
        [Inject(Id = "exitLevelCollider")] BoxCollider2D exitLevelBoxCollider,
        AssetReferenceSpawner spawner, 
        Settings settings)
    {
        this.exitLevelBoxCollider = exitLevelBoxCollider;
        this.spawner = spawner;
        this.settings = settings;
    }

    public void Initialize()
    {
        lastRockSpawnTime = Time.time;
        InstantiateStartRocks();
    }

    public async void Tick()
    {
        if (lastRockSpawnTime + settings.timeBetweenRockSpawns < Time.time)
            await InstantiateRock();
    }

    public async void InstantiateStartRocks()
    {
        for (int i = 0; i < settings.startRocks; i++)
            await InstantiateRock();
    }

    async Task InstantiateRock()
    {
        GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation);
        await spawner.Spawn(settings.rockPrefabReference, position, rotation);
        lastRockSpawnTime = Time.time;
    }

    void GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation)
    {
        var randomPointInLevel = RandomPointInLevel();

        float randomRotation = Random.Range(0, 360);
        
        var rotationQuaternion = Quaternion.Euler(0, 0, randomRotation);
        var randomRotationVector = (Vector2)(rotationQuaternion * Vector2.up);
        var startX = DistanceToClosestHorizontalEdge(randomRotation, randomPointInLevel);
        var startY = DistanceToClosestVerticalEdge(randomRotation, randomPointInLevel);

        var startDistance = Mathf.Min(startX, startY) + settings.startDistanceFromEdge ;

        position = randomPointInLevel + randomRotationVector * startDistance;
        rotation = Quaternion.Euler(0, 0, randomRotation) * Quaternion.Euler(0, 0, 180);
    }

    float DistanceToClosestVerticalEdge(float rotation, Vector2 position)
    {
        var cosRotation = Mathf.Cos(rotation * Mathf.Deg2Rad);
        if (cosRotation == 0)
            return float.MaxValue;
        else if(cosRotation > 0 )
            return (exitLevelBoxCollider.bounds.max.y - position.y) /cosRotation;
        else
            return (exitLevelBoxCollider.bounds.min.y - position.y) /cosRotation;
    }

    float DistanceToClosestHorizontalEdge(float rotation, Vector2 position)
    {
        var sinRotation = Mathf.Sin(rotation * Mathf.Deg2Rad);
        if (sinRotation == 0)
            return float.MaxValue;
        else if (sinRotation > 0)
            return (position.x - exitLevelBoxCollider.bounds.min.x) /sinRotation;
        else
            return (position.x - exitLevelBoxCollider.bounds.max.x)/sinRotation;

    }

    Vector2 RandomPointInLevel()
    {
        var levelBounds = exitLevelBoxCollider.bounds;
        return new Vector2(Random.Range(levelBounds.min.x, levelBounds.max.x), Random.Range(levelBounds.min.y, levelBounds.max.y));
    }

    [System.Serializable]
    public class Settings
    {
        public AssetReferenceGameObject rockPrefabReference = null;
        public int startRocks = 0;
        public float timeBetweenRockSpawns = 0;
        public float startDistanceFromEdge = 2;
    }
}
