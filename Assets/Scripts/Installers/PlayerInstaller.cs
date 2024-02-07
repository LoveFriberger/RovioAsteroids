using System.Collections;
using System;
using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    PlayerObject playerObject = null;
    [SerializeField]
    Settings settings = null;

    public override void InstallBindings()
    {
        Container.Bind<PlayerModel>().AsSingle().WithArguments(settings.rigidbody, settings.projectileSpawnTransform);

        Container.BindInterfacesTo<PlayerMover>().AsSingle();
        Container.BindInterfacesTo<PlayerShooter>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerDamageTaker>().AsSingle();

        Container.BindInstances(playerObject);
    }

    [Serializable]
    public class Settings
    {
        public Rigidbody2D rigidbody;
        public Transform projectileSpawnTransform;
    }
}
