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
    Player playerPrefab = null;

    [Space(), Header("Rock Spawner")]
    [SerializeField]
    Rock rockPrefab = null;
    [SerializeField]
    BoxCollider2D levelCollider = null;
    [SerializeField]
    int startRocks = 0;
    [SerializeField]
    float startVelocity = 0;
    [SerializeField]
    float timeBetweenRockSpawns = 0;
    [SerializeField]
    Transform rocksParent = null;
    [SerializeField]
    AssetReferenceGameObject rockPrefabReference = null;


    public override void InstallBindings()
    {
        //Player Spawner
        Container.BindFactory<Player, Player.Factory>().FromComponentInNewPrefab(playerPrefab);
        Container.BindInstance(playerStart).WhenInjectedInto<PlayerSpawner>();

        //Rock Spawner
        Container.BindFactory<Rock, Rock.Factory>().FromComponentInNewPrefab(rockPrefab); 
        Container.BindInstance(levelCollider).WhenInjectedInto<RockSpawner>();
        Container.BindInstance(startRocks).WhenInjectedInto<RockSpawner>();
        Container.BindInstance(startVelocity).WhenInjectedInto<RockSpawner>();
        //Container.BindInstance(timeBetweenRockSpawns).WhenInjectedInto<RockSpawner>();
        Container.BindInstance(rocksParent).WhenInjectedInto<RockSpawner>();
        Container.BindInstance(rockPrefabReference).WhenInjectedInto<RockSpawner>();

        //Other
        Container.BindInstance(levelCollider).WhenInjectedInto<ExitLevelCollider>();
    }
}
