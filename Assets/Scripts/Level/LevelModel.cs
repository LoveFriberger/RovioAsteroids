using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelModel
{
    readonly AssetReferenceSpawnerObject assetReferenceSpawner = null;
    readonly BoxCollider2D levelCollider = null;

    public LevelModel(AssetReferenceSpawnerObject assetReferenceSpawner, BoxCollider2D levelCollider)
    {
        this.assetReferenceSpawner = assetReferenceSpawner;
        this.levelCollider = levelCollider;
    }

    public AssetReferenceSpawnerObject AssetReferenceSpawner { get { return assetReferenceSpawner; } }

    public float LastRockSpawnTime { get; set; }

    public BoxCollider2D ExitLevelCollider { get { return levelCollider; }}

    public Vector2 ExitLevelColliderSize { get { return levelCollider.size; } set { levelCollider.size = value; } }

    public Bounds ExitLevelColliderBounds { get { return levelCollider.bounds; } }

    public Dictionary<string, GameObject> LoadedAssets { get; set; } = new();
}
