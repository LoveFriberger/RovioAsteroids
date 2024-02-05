using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RockModel
{
    AssetReferenceGameObject smallerRock = null;
    CircleCollider2D circleCollider = null;
    Rigidbody2D rigidbody = null;

    public RockModel(RockInstaller.Settings settings)
    {
        this.smallerRock = settings.smallerRock;
        this.circleCollider = settings.rockCollider;
        this.rigidbody = settings.rigidbody;
    }

    public AssetReferenceGameObject SmallerRock { get { return smallerRock; } }

    public Rigidbody2D Rigidbody { get { return rigidbody; } }

    public float SplitRocksDistanceFromCenter { get { return circleCollider.radius; } }

    public Vector2 Position { get { return rigidbody.transform.position; } }

    public Quaternion Rotation { get { return rigidbody.transform.rotation; } set { rigidbody.transform.rotation = value; } }

    public Vector2 Up { get { return rigidbody.transform.up; } }

    public Vector2 Right { get { return rigidbody.transform.right; } }

    public Vector2 Velocity { get { return rigidbody.velocity; } set { rigidbody.velocity = value; } }
}
