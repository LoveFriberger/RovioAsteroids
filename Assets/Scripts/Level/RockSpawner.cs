using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
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

    /*Takes a random point inside the level and a random rotation. Looks in the direction of the rotation 
     * how far the distance to the closest border is. Then it moves along that distance with some extra
     * margin to spawn just outside of the play area. The rock is the rotate towards the random starting
     * point
     */
    void GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation)
    {
        //Starting point
        var randomPointInLevel = RandomPointInLevel();
        float randomRotation = Random.Range(0, 360);
        
        //Random rotation from starting point
        var rotationQuaternion = Quaternion.Euler(0, 0, randomRotation);
        var randomRotationVector = (Vector2)(rotationQuaternion * Vector2.up);

        //See how far we have to walk in the random rotation to hit each edge
        var startX = DistanceToClosestHorizontalEdge(randomRotation, randomPointInLevel);
        var startY = DistanceToClosestVerticalEdge(randomRotation, randomPointInLevel);

        //Tak the shortest distance and add a margin
        var startDistance = Mathf.Min(startX, startY) + settings.startDistanceFromEdge;

        position = randomPointInLevel + randomRotationVector * startDistance;
        rotation = Quaternion.Euler(0, 0, randomRotation) * Quaternion.Euler(0, 0, 180);
    }

    float DistanceToClosestVerticalEdge(float rotation, Vector2 position)
    {
        var cosRotation = Mathf.Cos(rotation * Mathf.Deg2Rad);

        //If Cos(rotation) = 0 then the vertical edge is infinitely far away
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

        //If Sin(rotation) = 0 then the horizontal edge is infinitely far away
        if (sinRotation == 0)
            return float.MaxValue;
        else if (sinRotation > 0)
            return (position.x - exitLevelBoxCollider.bounds.min.x) /sinRotation;
        else
            return (position.x - exitLevelBoxCollider.bounds.max.x) /sinRotation;

    }

    Vector2 RandomPointInLevel()
    {
        var levelBounds = exitLevelBoxCollider.bounds;
        return new Vector2(Random.Range(levelBounds.min.x, levelBounds.max.x), Random.Range(levelBounds.min.y, levelBounds.max.y));
    }

    [System.Serializable]
    public class Settings
    {
        public AssetReferenceGameObject rockPrefabReference;
        public int startRocks = 0;
        public float timeBetweenRockSpawns = 0;
        public float startDistanceFromEdge = 2;
    }
}
