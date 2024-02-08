using System;
using UnityEngine;
using Zenject;

public class PlayerMover : IFixedTickable
{
    readonly PlayerModel playerModel;
    readonly InputView inputView;
    readonly Settings settings;

    public PlayerMover(PlayerModel playerModel, InputView inputView, Settings settings)
    {
        this.playerModel = playerModel;
        this.inputView = inputView;
        this.settings = settings;
    }

    public void FixedTick()
    {
        if (inputView.InputType != InputModel.Type.Player)
            return;

        if (inputView.UpInputHold)
            Accelerate();
        if (inputView.LeftInputHold)
            Turn(true);
        if (inputView.RightInputHold)
            Turn(false);
    }


    /*
     * Force is modified depending on how close we are to the max speed. However we only want to reduce the force
     * when moving in the direction of the velocity, if we are at full speed and turn our player around 180 degrees 
     * we want to get the full movement force. Thats why we add another modifier depending on the angle between our 
     * forward and the current velocity. 
     */
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
        var turnAmount = settings.turnSpeed * Time.fixedDeltaTime * turnDirection ;
        playerModel.Rotation *= Quaternion.Euler(0, 0, turnAmount);
    }

    [Serializable]
    public class Settings
    {
        public float accelerationForce = 10;
        public float maxVelocity = 5;
        public float turnSpeed = 50;
    }
}
