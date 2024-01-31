using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{ 
    [SerializeField]
    float accelerationForce = 10;
    [SerializeField]
    float maxVelocity = 5;
    [SerializeField]
    float turnSpeed = 50;
    [SerializeField]
    Rigidbody2D playerRigidbody = null;
    [SerializeField]
    InputActionAsset inputActionAsset = null;

    Vector2 movementInputValues = new();

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        movementInputValues = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (movementInputValues.y > 0)
            Accelerate();
        if (movementInputValues.x != 0)
            Turn(movementInputValues.x < 0);
    }

    void Accelerate()
    {
        var signedAngle = Vector2.SignedAngle(transform.up, playerRigidbody.velocity);
        var angleModifier = Mathf.Abs(signedAngle) < 90 ? Mathf.Cos(signedAngle * Mathf.Deg2Rad) : 0;

        var forceModifier = 1 - (angleModifier * playerRigidbody.velocity.sqrMagnitude) / (maxVelocity * maxVelocity);
        playerRigidbody.AddRelativeForceY(forceModifier * accelerationForce);
    }

    void Turn(bool clockwise)
    {
        var turnDirection = clockwise ? 1 : -1;
        var turnAmount = turnSpeed * Time.deltaTime * turnDirection ;
        transform.localRotation *= Quaternion.Euler(0, 0, turnAmount);
    }

}
