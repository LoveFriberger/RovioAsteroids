using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class PlayerShooter : ITickable
{
    readonly InputView inputView;
    readonly IAssetReferenceSpawner projectileSpawner;
    readonly PlayerModel playerModel;
    readonly Settings settings;

    public PlayerShooter(PlayerModel playerModel, IAssetReferenceSpawner projectileSpawner, InputView inputView, Settings settings)
    {
        this.playerModel = playerModel;
        this.projectileSpawner = projectileSpawner;
        this.inputView = inputView;
        this.settings = settings;
    }


    public void Tick()
    {
        if (inputView.InputType != InputModel.Type.Player)
            return;

        if (inputView.ActionInputDown && playerModel.TimeLastShot + settings.cooldown < Time.time)
            Shoot();
    }

    async void Shoot()
    {
        Debug.Log("Player shooting");
        playerModel.TimeLastShot = Time.time;

        await projectileSpawner.Spawn(
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
        public AssetReferenceGameObject projectilePrefabReference;
    }
}
