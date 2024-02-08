using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        GivePoints();

        if (rockModel.SmallerRock.RuntimeKeyIsValid())
            await SpawnSmallerRocks();
    }

    async Task SpawnSmallerRocks()
    {
        var firstSpawnPosition = rockModel.Position + rockModel.Right * rockModel.SplitRocksDistanceFromCenter;
        var secondSpawnPosition = rockModel.Position - rockModel.Right * rockModel.SplitRocksDistanceFromCenter;

        var bigRockRightDirection = rockModel.Right;
        var smallerRock = rockModel.SmallerRock;
        var rotation = rockModel.Rotation;

        await spawner.Spawn(
            smallerRock,
            firstSpawnPosition,
            rotation,
            (r) =>
            {
                r.GetComponent<RockObject>().AddVelocity(bigRockRightDirection * settings.splitSpeed);
            });

        await spawner.Spawn(
            smallerRock,
            secondSpawnPosition,
            rotation,
            (r) =>
            {
                r.GetComponent<RockObject>().AddVelocity(-bigRockRightDirection * settings.splitSpeed);
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
