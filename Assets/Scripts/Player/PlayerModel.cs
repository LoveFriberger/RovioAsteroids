using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    readonly Rigidbody2D rigidbody = null;
    readonly AssetReferenceSpawnerObject assetReferenceSpawner = null;

    public PlayerModel(PlayerInstaller.Settings settings)
    {
        this.rigidbody = settings.rigidbody;
        this.assetReferenceSpawner = settings.assetReferenceSpawner;
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

    public AssetReferenceSpawnerObject AssetReferenceSpawner { get { return assetReferenceSpawner; } }

    public Transform projectileSpawnPosition { get { return assetReferenceSpawner.transform; } }
}
