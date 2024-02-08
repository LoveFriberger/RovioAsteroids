using System;
using UnityEngine;
using Zenject;

public class RockMover : IInitializable
{
    readonly RockModel rockModel;
    readonly Settings settings;

    public RockMover(RockModel rockModel, Settings settings)
    {
        this.rockModel = rockModel;
        this.settings = settings;
    }

    public void Initialize()
    {
         AddVelocity(rockModel.Up * settings.initSpeed);
    }

    public void AddVelocity(Vector2 velocity)
    {
        Debug.Log(string.Format("Added velocity {0} to rock", velocity));
        rockModel.Velocity += velocity;
    }

    [Serializable]
    public class Settings
    {
        public float initSpeed = 3;
    }
}
