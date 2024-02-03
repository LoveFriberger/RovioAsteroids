using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

public class LevelInstaller : MonoInstaller
{
    [Header("Player Spawner")]
    [SerializeField]
    Transform playerStart = null;
    [SerializeField]
    AssetReferenceGameObject playerPrefabReference = null;

    [Space(), Header("Rock Spawner")]
    [SerializeField]
    BoxCollider2D levelCollider = null;
    [SerializeField]
    Transform rocksParent = null;
    [SerializeField]
    int startRocks = 0;
    [SerializeField]
    float timeBetweenRockSpawns = 0;
    [SerializeField]
    AssetReferenceGameObject rockPrefabReference = null;

    public override void InstallBindings()
    {
        var playerSpawnerSettings = new PlayerSpawner.Settings()
        {
            playerStart = playerStart,
            playerPrefabReference = playerPrefabReference
        };
        Container.BindInstance(playerSpawnerSettings);

        var rockSpawnerSettings = new RockSpawner.Settings()
        {
            startRocks = startRocks,
            timeBetweenRockSpawns = timeBetweenRockSpawns,
            rocksParent = rocksParent,
            exitLevelCollider = levelCollider,
            rockPrefabReference = rockPrefabReference
        };
        Container.BindInstance(rockSpawnerSettings);

        //Other
        Container.BindInstance(levelCollider).WhenInjectedInto<ExitLevelCollider>();
    }
}
