using UnityEngine;
using Zenject;
using NUnit.Framework;
using UnityEngine.AddressableAssets;
using UnityEditor;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class Player : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();

        var projectile = new AssetReferenceGameObject(AssetDatabase.AssetPathToGUID("Assets/Prefabs/Projectile.prefab"));
        var player = new AssetReferenceGameObject(AssetDatabase.AssetPathToGUID("Assets/Prefabs/Player.prefab"));
        
        var rigidbody = new GameObject().AddComponent<Rigidbody2D>();
        var transform = new GameObject().transform;
        Container.BindInstance(transform);

        var spawnerGameObject = new GameObject().AddComponent<AssetReferenceSpawnerObject>();
        Container.Inject(spawnerGameObject);
        Container.BindInstance(spawnerGameObject).AsSingle();
        Container.Bind<AssetReferenceSpawner>().AsTransient().WithArguments(spawnerGameObject);

        Container.Bind<PlayerModel>().AsSingle().WithArguments(rigidbody, rigidbody.transform);
        Container.Bind<PlayerMover.Settings>().AsSingle();
        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.Bind<PlayerDamageTaker>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMover>().AsSingle();

        Container.Bind<PlayerShooter.Settings>().AsSingle().OnInstantiated((i, o) => {
            var settings = (o as PlayerShooter.Settings);
            settings.projectilePrefabReference = projectile;
            settings.cooldown = -5;
        });
        Container.BindInterfacesAndSelfTo<PlayerShooter>().AsSingle();

        Container.Bind<PlayerSpawner.Settings>().AsSingle().OnInstantiated((i, o) => { (o as PlayerSpawner.Settings).playerPrefabReference = player; });
        Container.Bind<PlayerSpawner>().AsSingle();
        Container.Inject(this);
    }

    [Inject]
    PlayerMover playerMover;
    [Inject]
    InputModel inputModel;
    [Inject]
    PlayerModel playerModel;
    [Inject]
    PlayerDamageTaker playerDamageTaker;
    [Inject]
    GameManagerController gameManagerController;

    [Test]
    public void Turn()
    {
        inputModel.inputType = InputModel.Type.Player;

        playerModel.Rotation = Quaternion.Euler(0, 0, 40);
        inputModel.leftInputHold = true;
        playerMover.FixedTick();
        inputModel.leftInputHold = false;
;
        Assert.True(playerModel.Rotation.eulerAngles.z > 40);

        playerModel.Rotation = Quaternion.Euler(0, 0, 40);
        inputModel.rightInputHold = true;
        playerMover.FixedTick();
        Assert.True(playerModel.Rotation.eulerAngles.z < 40);
        inputModel.rightInputHold = false;
    }
    
    
    [Test]
    public void TakeDamage()
    {
        var playerKilled = false;
        gameManagerController.AddPlayerKilledAction(() => playerKilled = true);
        playerDamageTaker.TakeDamage();
        Assert.IsTrue(playerKilled);
    }
}
