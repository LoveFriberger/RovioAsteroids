using UnityEngine;
using Zenject;
using NUnit.Framework;
using UnityEngine.AddressableAssets;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class Rock : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();

        Container.Bind<PointsModel>().AsSingle();
        Container.Bind<PointsView>().AsSingle();
        Container.Bind<PointsController>().AsSingle();

        var rockLarge = new AssetReferenceGameObject(AssetDatabase.AssetPathToGUID("Assets/Prefabs/RockLarge.prefab"));
        var rockMedium = new AssetReferenceGameObject(AssetDatabase.AssetPathToGUID("Assets/Prefabs/RockMedium.prefab"));
        var rockSmall = new AssetReferenceGameObject(AssetDatabase.AssetPathToGUID("Assets/Prefabs/RockSmall.prefab"));

        var circleCollider = new GameObject().AddComponent<CircleCollider2D>();
        var rigidBody = circleCollider.gameObject.AddComponent<Rigidbody2D>();
        var rockObject = rigidBody.gameObject.AddComponent<RockObject>();
        Container.BindInstance(rockObject).AsSingle();

        var levelCollider = new GameObject().AddComponent<BoxCollider2D>();
        Container.BindInstance(levelCollider).WithId("exitLevelCollider").AsSingle();

        var spawnerGameObject = new GameObject().AddComponent<AssetReferenceSpawnerObject>();
        Container.Inject(spawnerGameObject);
        Container.BindInstance(spawnerGameObject).AsSingle();
        Container.Bind<AssetReferenceSpawner>().AsSingle().WithArguments(spawnerGameObject);

        Container.Bind<RockInstaller>().AsSingle();
        Container.Bind<RockModel>().AsSingle().WithArguments(rockMedium, circleCollider, rigidBody);
        Container.Bind<RockMover.Settings>().AsSingle();
        Container.BindInterfacesAndSelfTo<RockMover>().AsSingle();
        Container.Bind<RockDamageTaker>().AsSingle(); 
        Container.Bind<RockDamageTaker.Settings>().AsSingle();

        var transform = new GameObject().transform;
        Container.BindInstance(transform);
        Container.Bind<RockSpawner.Settings>().AsSingle().OnInstantiated((i, o) => {
            var settings = o as RockSpawner.Settings;
            settings.rockPrefabReference = rockLarge;
            settings.timeBetweenRockSpawns = 0;
        });
        Container.Bind<RockSpawner>().AsSingle();

        Container.Inject(this);
    }


    [Inject]
    RockMover rockMover;
    [Inject]
    RockSpawner rockSpawner;
    [Inject]
    RockSpawner.Settings rockSpawnerSettings;
    [Inject]
    AssetReferenceSpawnerObject assetReferenceSpawnerObject;
    [Inject]
    RockMover.Settings rockMoverSettings;
    [Inject]
    RockObject rockObject;
    [Inject]
    RockDamageTaker rockDamageTaker;
    [Inject]
    PointsModel pointsModel;

    float assetLoadingTimeout = 10;

    [Test]
    public void InitialSpeed()
    {
        rockMover.Initialize();
        var rigidbody = rockObject.GetComponent<Rigidbody2D>();
        Assert.True(rigidbody != null);
        Assert.True(rigidbody.velocity.magnitude == rockMoverSettings.initSpeed);
    }

    [Test]
    public void TakeDamage()
    {
        pointsModel.currentPoints = 0;
        rockDamageTaker.TakeDamage();
        Assert.That(pointsModel.currentPoints == 1);
    }

    [UnityTest]
    public IEnumerator Spawn()
    {
        Assert.True(assetReferenceSpawnerObject.transform.childCount == 0);
        rockSpawner.Tick();

        //Wait some frames to make sure the asset is loaded
        var assetLoadingStartTime = Time.realtimeSinceStartup;
        while(assetReferenceSpawnerObject.transform.childCount == 0)
        {
            yield return null;
            if(Time.realtimeSinceStartup > assetLoadingStartTime + assetLoadingTimeout)
            {
                Debug.LogWarning("Loading timed out when trying to load" + rockSpawnerSettings.rockPrefabReference.editorAsset.name);
                Assert.False(true);
            }
        }

        Assert.True(assetReferenceSpawnerObject.transform.childCount > 0);
        Assert.True(assetReferenceSpawnerObject.transform.GetChild(0).GetComponent<RockObject>() != null);
    }
}
