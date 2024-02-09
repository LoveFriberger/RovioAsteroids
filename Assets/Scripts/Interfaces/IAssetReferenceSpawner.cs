using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

public interface IAssetReferenceSpawner
{
    Task Spawn(AssetReferenceGameObject asset, Vector2 startPosition, Quaternion startRotation, Action<GameObject> onSpawnedObject = null);
}
