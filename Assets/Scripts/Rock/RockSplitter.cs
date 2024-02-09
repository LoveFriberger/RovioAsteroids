using System;
using System.Threading.Tasks;
using UnityEngine;

public class RockSplitter
{
    readonly RockModel rockModel;
    readonly IAssetReferenceSpawner spawner;
    readonly Settings settings;

    public RockSplitter(RockModel rockModel, IAssetReferenceSpawner spawner, Settings settings)
    {
        this.rockModel = rockModel;
        this.spawner = spawner;
        this.settings = settings;
    }

    public async Task Split()
    {
        if (!rockModel.SmallerRock.RuntimeKeyIsValid())
            return;

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
    [Serializable]
    public class Settings
    {
        public float splitSpeed = 0.5f;
    }
}
