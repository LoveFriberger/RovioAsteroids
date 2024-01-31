using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField]
    float accelerationForce = 10;
    [SerializeField]
    float maxVelocity = 5;
    [SerializeField]
    float turnSpeed = 50;


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
           Accelerate();
        if (Input.GetKey(KeyCode.LeftArrow))
            Turn(true);
        else if (Input.GetKey(KeyCode.RightArrow))
            Turn(false);
    }

    void Accelerate()
    {
        var signedAngle = Vector2.SignedAngle(transform.up, GetComponent<Rigidbody2D>().velocity);
        var angleModifier = Mathf.Abs(signedAngle) < 90 ? Mathf.Cos(signedAngle * Mathf.Deg2Rad) : 0;

        var forceModifier = 1 - (angleModifier * GetComponent<Rigidbody2D>().velocity.sqrMagnitude) / (maxVelocity * maxVelocity);
        GetComponent<Rigidbody2D>().AddRelativeForceY(forceModifier * accelerationForce);
    }

    void Turn(bool clockwise)
    {
        var turnDirection = clockwise ? 1 : -1;
        var turnAmount = turnSpeed * Time.deltaTime * turnDirection ;
        transform.localRotation *= Quaternion.Euler(0, 0, turnAmount);
    }

}
