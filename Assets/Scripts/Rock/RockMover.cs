using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RockMover : IInitializable
{
    readonly RockModel rockModel = null;
    readonly Settings settings = null;

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
        rockModel.Velocity += velocity;
    }

    [Serializable]
    public class Settings
    {
        public float initSpeed = 3;
    }
}
