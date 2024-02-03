using System.Collections;
using System;
using UnityEngine;

public class LevelModel
{
    readonly AssetReferenceSpawner assetReferenceSpawner = null;
    readonly BoxCollider2D levelCollider = null;

    public LevelModel(AssetReferenceSpawner assetReferenceSpawner, BoxCollider2D levelCollider)
    {
        this.assetReferenceSpawner = assetReferenceSpawner;
        this.levelCollider = levelCollider;
    }

    public AssetReferenceSpawner AssetReferenceSpawner { get { return assetReferenceSpawner; } }

    public float LastRockSpawnTime { get; set; }

    public BoxCollider2D ExitLevelCollider { get { return levelCollider; }}

    public Vector2 ExitLevelColliderSize { get { return levelCollider.size; } set { levelCollider.size = value; } }

    public Bounds ExitLevelColliderBounds { get { return levelCollider.bounds; } }
}
