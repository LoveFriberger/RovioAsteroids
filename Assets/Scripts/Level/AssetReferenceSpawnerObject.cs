using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetReferenceSpawnerObject : MonoBehaviour
{
    
    public void Spawn(GameObject gameObject, Transform parent = null, Action<GameObject> onSpawnedObject = null)
    {
        Spawn(gameObject, transform.position, transform.rotation, parent, onSpawnedObject);
    }

    public void Spawn(GameObject gameObject, Vector2 startPosition, Quaternion startRotation, Transform parent = null, Action<GameObject> onSpawnedObject = null)
    {
        var spawnedObject = Instantiate(gameObject, startPosition, startRotation, parent);
        onSpawnedObject?.Invoke(spawnedObject);
    }
}
