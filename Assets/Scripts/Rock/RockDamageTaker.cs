using System;
using System.Threading.Tasks;
using UnityEngine;

public class RockDamageTaker
{
    readonly RockModel rockModel;
    readonly IAssetReferenceSpawner spawner;
    readonly PointsController pointsController;
    readonly Settings settings;

    public RockDamageTaker(RockModel rockModel, IAssetReferenceSpawner spawner, PointsController pointsController, Settings settings)
    {
        this.rockModel = rockModel;
        this.spawner = spawner;
        this.pointsController = pointsController;
        this.settings = settings;
    }

    public async void TakeDamage()
    {
        Debug.Log("A rock is taking damage");
        GivePoints();

        if (rockModel.SmallerRock.RuntimeKeyIsValid())
            await SpawnSmallerRocks();
    }

    async Task SpawnSmallerRocks()
    {
        Debug.Log("Spawning smaller rocks");

        var firstSpawnPosition = rockModel.Position + rockModel.Right * rockModel.SplitRocksDistanceFromCenter;
        var secondSpawnPosition = rockModel.Position - rockModel.Right * rockModel.SplitRocksDistanceFromCenter;

        //Since the spawn function is async we have to copy the values from rockModel before it's destroyed
        var smallerRock = rockModel.SmallerRock;
        var rotation = rockModel.Rotation;
        var forwardDirection = rockModel.Right;

        await spawner.Spawn(
            smallerRock,
            firstSpawnPosition,
            rotation,
            (r) =>
            {
                r.GetComponent<RockObject>().AddVelocity(forwardDirection * settings.splitSpeed);
            });

        await spawner.Spawn(
            smallerRock,
            secondSpawnPosition,
            rotation,
            (r) =>
            {
                r.GetComponent<RockObject>().AddVelocity(-forwardDirection * settings.splitSpeed);
            });
    }

    void GivePoints()
    {
        pointsController.AddPoints(settings.points);
    }

    [Serializable]
    public class Settings
    {
        public int points = 1;
        public float splitSpeed = 0.5f;
    }
}
