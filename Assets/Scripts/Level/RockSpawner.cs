using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

public class RockSpawner : IInitializable, ITickable
{
    readonly LevelModel levelModel = null;
    readonly AssetReferenceSpawner spawner = null;
    readonly Settings settings = null;
    readonly GameController gameController = null;

    public RockSpawner(LevelModel levelModel, AssetReferenceSpawner spawner, Settings settings, GameController gameController)
    {
        this.levelModel = levelModel;
        this.spawner = spawner;
        this.settings = settings;
        this.gameController = gameController;
    }

    public void Initialize()
    {
        levelModel.LastRockSpawnTime = Time.time;
        InstantiateStartRocks();
        gameController.AddResetGameAction(InstantiateStartRocks);
    }

    public void RemoveOnResetAction()
    {
        gameController.RemoveResetGameAction(InstantiateStartRocks);
    }

    public async void Tick()
    {
        if (levelModel.LastRockSpawnTime + settings.timeBetweenRockSpawns < Time.time)
            await InstantiateRock();
    }

    async void InstantiateStartRocks()
    {
        for (int i = 0; i < settings.startRocks; i++)
            await InstantiateRock();
    }

    async Task InstantiateRock()
    {
        GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation);
        await spawner.Spawn(settings.rockPrefabReference, position, rotation);
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
