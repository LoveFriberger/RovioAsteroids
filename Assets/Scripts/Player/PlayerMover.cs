using System.Collections;
using System;
using UnityEngine;
using Zenject;

public class PlayerMover : IFixedTickable
{
    readonly PlayerModel playerModel = null;
    readonly LevelModel levelModel = null;
    readonly InputView inputView = null;
    readonly Settings settings = null;

    public PlayerMover(PlayerModel playerModel, LevelModel levelModel, InputView inputView, Settings settings)
    {
        this.playerModel = playerModel;
        this.levelModel = levelModel;
        this.inputView = inputView;
        this.settings = settings;
    }

    public void FixedTick()
    {
        if (levelModel.MenuObjectActivated)
            return;

        if (inputView.UpInputHold)
            Accelerate();
        if (inputView.LeftInputHold)
            Turn(true);
        if (inputView.RightInputHold)
            Turn(false);
    }

    void Accelerate()
    {
        var signedAngle = Vector2.SignedAngle(playerModel.MovementDirecetion, playerModel.Velocity);
        var angleModifier = Mathf.Abs(signedAngle) < 90 ? Mathf.Cos(signedAngle * Mathf.Deg2Rad) : 0;

        var forceModifier = 1 - (angleModifier * playerModel.Velocity.sqrMagnitude) / (settings.maxVelocity * settings.maxVelocity);
        playerModel.Rigidbody.AddRelativeForceY(forceModifier * settings.accelerationForce);
    }

    void Turn(bool clockwise)
    {
        var turnDirection = clockwise ? 1 : -1;
        var turnAmount = settings.turnSpeed * Time.deltaTime * turnDirection ;
        playerModel.LocalRotation *= Quaternion.Euler(0, 0, turnAmount);
    }

    [Serializable]
    public class Settings
    {

        public float accelerationForce = 10;
        public float maxVelocity = 5;
        public float turnSpeed = 50;
    }
}
