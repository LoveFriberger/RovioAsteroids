using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class PlayerShooter : ITickable
{
    readonly InputView inputView = null;
    readonly LevelModel levelModel = null;
    readonly AssetReferenceSpawner spawner;
    readonly PlayerModel playerModel = null;
    readonly Settings settings = null;

    public PlayerShooter(PlayerModel playerModel, LevelModel levelModel, AssetReferenceSpawner spawner, InputView inputView, Settings settings)
    {
        this.playerModel = playerModel;
        this.levelModel = levelModel;
        this.spawner = spawner;
        this.inputView = inputView;
        this.settings = settings;
    }


    public void Tick()
    {
        if (levelModel.MenuObjectActivated)
            return;
        if (inputView.ActionInputDown)
            Debug.Log(playerModel.TimeLastShot + settings.cooldown < Time.time);
        if (inputView.ActionInputDown && playerModel.TimeLastShot + settings.cooldown < Time.time)
            Shoot();
    }

    async void Shoot()
    {
        playerModel.TimeLastShot = Time.time;

        await spawner.Spawn(
            settings.projectilePrefabReference, 
            playerModel.projectileSpawnPosition.position,
            playerModel.projectileSpawnPosition.rotation, 
            (p) => AddVelocityToProjectile(p));

    }

    void AddVelocityToProjectile(GameObject projectileClone)
    {
        projectileClone.GetComponent<Rigidbody2D>().velocity = playerModel.Velocity + (Vector2)projectileClone.transform.up * settings.projectileVelocity;
    }

    [Serializable]
    public class Settings
    {
        public float cooldown = 0.5f;
        public float projectileVelocity = 5;
        public AssetReferenceGameObject projectilePrefabReference = null;
    }
}
