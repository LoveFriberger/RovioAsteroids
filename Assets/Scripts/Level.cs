using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    int startRocks = 3;
    [SerializeField]
    GameObject rockPrefab = null;
    [SerializeField]
    float startVelocity = 3;
    [SerializeField]
    ExitLevelCollider exitLevelCollider = null;

    float timeBetweenSpawns = 5;
    float lastRockSpawnTime = 0;

    private void Start()
    {
        for (int i = 0; i < startRocks; i++)
            InstantiateRock();
    }

    void Update()
    {
        if (lastRockSpawnTime + timeBetweenSpawns < Time.time)
            InstantiateRock();
    }

    void InstantiateRock()
    {
        GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation);
        var rockClone = Instantiate(rockPrefab, position, rotation);
        rockClone.GetComponent<Rigidbody2D>().velocity = rockClone.transform.up * startVelocity;
        lastRockSpawnTime = Time.time;
    }

    void GetRockStartPositionAndRotation(out Vector2 position, out Quaternion rotation)
    {
        var randomPointInLevel = RandomPointInLevel();
        var randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        var randomRotationVector = (Vector2)(randomRotation * Vector2.up);
        var startDistance = exitLevelCollider.GetComponent<Collider2D>().bounds.size.magnitude * 1.2f;

        position = randomPointInLevel + randomRotationVector * startDistance;
        rotation = randomRotation * Quaternion.Euler(0, 0, 180);
    }

    Vector2 RandomPointInLevel()
    {
        var levelBounds = exitLevelCollider.GetComponent<Collider2D>().bounds;
        return new Vector2(Random.Range(levelBounds.min.x, levelBounds.max.x), Random.Range(levelBounds.min.y, levelBounds.max.y));
    }
}
