using System;
using System.Threading.Tasks;
using UnityEngine;

public class RockDamageTaker
{
    readonly RockModel rockModel;
    readonly AssetReferenceSpawner spawner;
    readonly PointsController pointsController;
    readonly Settings settings;

    public RockDamageTaker(RockModel rockModel, AssetReferenceSpawner spawner, PointsController pointsController, Settings settings)
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

        await spawner.Spawn(
            rockModel.SmallerRock,
            firstSpawnPosition,
            rockModel.Rotation,
            (r) =>
            {
                r.GetComponent<RockObject>().AddVelocity(rockModel.Right * settings.splitSpeed);
            });

        await spawner.Spawn(
            rockModel.SmallerRock,
            secondSpawnPosition,
            rockModel.Rotation,
            (r) =>
            {
                r.GetComponent<RockObject>().AddVelocity(-rockModel.Right * settings.splitSpeed);
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
