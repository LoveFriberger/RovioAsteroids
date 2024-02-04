using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RockDamageTaker
{
    readonly RockModel rockModel = null;
    readonly AssetReferenceSpawner spawner = null;
    readonly PointsController pointsController = null;
    readonly Settings settings = null;

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
                (r) => {
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
    }
    void GivePoints()
    {
        pointsController.AddPoints(1);
    }

    [Serializable]
    public class Settings
    {
        public float splitSpeed = 0.5f;
    }
}
