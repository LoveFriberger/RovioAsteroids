using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class PlayerShooter : IFixedTickable
{
    readonly InputView inputView = null;
    readonly PlayerModel playerModel = null;
    readonly Settings settings = null;

    public PlayerShooter(PlayerModel playerModel, InputView inputView, Settings settings)
    {
        this.playerModel = playerModel;
        this.inputView = inputView;
        this.settings = settings;
    }


    public void FixedTick()
    {
        if (inputView.ActionInput)
            Shoot();
    }

    void Shoot()
    {
        if (playerModel.TimeLastShot + settings.cooldown > Time.time)
            return;

        playerModel.AssetReferenceSpawner.Spawn(settings.projectilePrefabReference, playerModel.projectileParent, (p) => AddVelocityToProjectile(p));

        playerModel.TimeLastShot = Time.time;
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
