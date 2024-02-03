using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    readonly Rigidbody2D rigidbody = null;
    readonly AssetReferenceSpawner assetReferenceSpawner = null;

    public PlayerModel(Rigidbody2D rigidbody, AssetReferenceSpawner assetReferenceSpawner)
    {
        this.rigidbody = rigidbody;
        this.assetReferenceSpawner = assetReferenceSpawner;
    }

    public Rigidbody2D Rigidbody { get { return rigidbody; } }

    public Vector2 MovementDirecetion { get { return rigidbody.transform.up; } }

    public Vector2 Velocity { get { return rigidbody.velocity; } }
    public Quaternion Rotation { get { return rigidbody.transform.rotation; } }
    public Quaternion LocalRotation
    {
        get
        {
            return rigidbody.transform.localRotation;
        }
        set
        {
            rigidbody.transform.localRotation = value;
        }
    }

    public float TimeLastShot { get; set; }

    public AssetReferenceSpawner AssetReferenceSpawner { get { return assetReferenceSpawner; } }

    public Transform projectileParent { get { return assetReferenceSpawner.transform; } }
}
