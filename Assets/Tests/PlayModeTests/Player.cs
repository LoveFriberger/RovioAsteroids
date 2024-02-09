using System.Collections;
using UnityEngine;
using Zenject;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;
using UnityEditor;

public class Player : ZenjectIntegrationTestFixture
{
    void Install()
    {
        PreInstall();

        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();
        var rigidbody = new GameObject().AddComponent<Rigidbody2D>();
        Container.Bind<PlayerModel>().AsSingle().WithArguments(rigidbody, rigidbody.transform);

        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMover>().AsSingle();

        var projectile = new AssetReferenceGameObject(AssetDatabase.AssetPathToGUID("Assets/Prefabs/Projectile.prefab"));
        var player = new AssetReferenceGameObject(AssetDatabase.AssetPathToGUID("Assets/Prefabs/Player.prefab"));

        var transform = new GameObject().transform;
        Container.BindInstance(transform);

        Container.Bind<PlayerShooter.Settings>().AsSingle().OnInstantiated((i, o) =>
        {
            var settings = (o as PlayerShooter.Settings);
            settings.projectilePrefabReference = projectile;
            settings.cooldown = -5;
        });

        Container.BindInterfacesAndSelfTo<PlayerShooter>().AsSingle();

        var spawnerGameObject = new GameObject().AddComponent<AssetReferenceSpawnerObject>();
        Container.Inject(spawnerGameObject);
        Container.BindInstance(spawnerGameObject).AsSingle();
        Container.Bind<AssetReferenceSpawner>().AsTransient().WithArguments(spawnerGameObject);


        Container.Bind<PlayerSpawner.Settings>().AsSingle().OnInstantiated((i, o) => 
        {
            var settings = (o as PlayerSpawner.Settings);
            settings.playerPrefabReference = player; 
        });
        Container.Bind<PlayerSpawner>().AsSingle();

        Container.Inject(this);

        PostInstall();
    }

    [Inject]
    InputModel inputModel;
    [Inject]
    PlayerModel playerModel;
    [Inject]
    PlayerShooter playerShooter;
    [Inject]
    AssetReferenceSpawnerObject assetReferenceSpawnerObject;
    [Inject]
    PlayerSpawner playerSpawner;

    float assetLoadingTimeout = 30;

    [UnityTest]
    public IEnumerator Accelerate()
    {
        Install();
        Assert.True(playerModel.Velocity.sqrMagnitude == 0);
        inputModel.inputType = InputModel.Type.Player;
        inputModel.upInputHold = true;
        yield return null;

        Assert.True(playerModel.Velocity.sqrMagnitude > 0);
    }

    [UnityTest]
    public IEnumerator Shoot()
    {
        Install();
        Assert.True(assetReferenceSpawnerObject.transform.childCount == 0);

        inputModel.inputType = InputModel.Type.Player;
        inputModel.actionInputDown = true;
        playerShooter.Tick();
        inputModel.actionInputDown = false;

        var assetLoadingStartTime = Time.realtimeSinceStartup;
        while (assetReferenceSpawnerObject.transform.childCount == 0)
        {
            yield return null;
            if (Time.realtimeSinceStartup > assetLoadingStartTime + assetLoadingTimeout)
            {
                Debug.LogWarning("Loading timed out when trying to load projectile");
                Assert.False(true);
            }
        }

        Debug.Log("Shoot " +assetReferenceSpawnerObject.transform.childCount);
        Assert.True(assetReferenceSpawnerObject.transform.childCount > 0);
        var projectile = assetReferenceSpawnerObject.transform.GetChild(0).GetComponent<Projectile>();
        Assert.True(projectile != null);
        Assert.True(projectile.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > 0);

        Object.DestroyImmediate(projectile);
    }

    [UnityTest]
    public IEnumerator Spawner()
    {
        Install();
        Assert.True(assetReferenceSpawnerObject.transform.childCount == 0);
        playerSpawner.Initialize();

        //Wait some frames to make sure the asset is loaded
        for (int i = 0; i < 500; i++)
            yield return null;

        Debug.Log("Spawn " + assetReferenceSpawnerObject.transform.childCount);
        Assert.True(assetReferenceSpawnerObject.transform.childCount == 1);
        var playerObject = assetReferenceSpawnerObject.transform.GetChild(0).GetComponent<PlayerObject>();
        Assert.True(assetReferenceSpawnerObject.transform.GetChild(0).GetComponent<PlayerObject>() != null);

        Object.DestroyImmediate(playerObject);
    }
}
