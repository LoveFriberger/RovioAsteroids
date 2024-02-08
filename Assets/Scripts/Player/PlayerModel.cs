using UnityEngine;

public class PlayerModel
{
    readonly Rigidbody2D rigidbody;
    readonly Transform projectileSpawnTransform;

    public PlayerModel(Rigidbody2D rigidbody, Transform projectileSpawnTransform)
    {
        this.rigidbody = rigidbody;
        this.projectileSpawnTransform = projectileSpawnTransform;
    }

    public Rigidbody2D Rigidbody { get { return rigidbody; } }

    public Vector2 MovementDirecetion { get { return rigidbody.transform.up; } }

    public Vector2 Velocity { get { return rigidbody.velocity; } }
    public Quaternion Rotation
    {
        get
        {
            return rigidbody.transform.rotation;
        }
        set
        {
            rigidbody.transform.rotation = value;
        }
    }

    public float TimeLastShot { get; set; }

    public Transform projectileSpawnPosition { get { return projectileSpawnTransform; } }
}
