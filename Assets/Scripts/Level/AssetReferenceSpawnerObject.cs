using System.Collections;
using System;
using UnityEngine;
using Zenject;

public class AssetReferenceSpawnerObject : MonoBehaviour
{
    GameManagerController gameController = null;

    [Inject]
    public void Construct(GameManagerController gameController)
    {
        this.gameController = gameController;
    }

    public void Spawn(GameObject gameObject, Vector2 startPosition, Quaternion startRotation, Action<GameObject> onSpawnedObject = null)
    {
        var spawnedObject = Instantiate(gameObject, startPosition, startRotation, transform);
        onSpawnedObject?.Invoke(spawnedObject);
    }

    void OnEnable()
    {
        gameController.AddResetGameAction(DestroyAllSpawnedObjects);
    }

    void OnDisable()
    {
        gameController.RemoveResetGameAction(DestroyAllSpawnedObjects);
    }

    void DestroyAllSpawnedObjects()
    {
        for (int i = transform.childCount -1 ; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
