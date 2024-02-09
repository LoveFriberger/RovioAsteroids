using System.Collections;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

public class RockInstaller : MonoInstaller
{
    [SerializeField]
    RockObject rockObject;
    [SerializeField]
    Settings settings;

    public override void InstallBindings()
    {
        Container.Bind<RockModel>().AsSingle().WithArguments(settings.smallerRock, settings.rockCollider, settings.rigidbody);

        Container.Bind<RockSplitter>().AsSingle();
        Container.Bind<RockDamageTaker>().AsSingle();
        Container.BindInterfacesAndSelfTo<RockMover>().AsSingle(); 

        Container.BindInstances(rockObject);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceGameObject smallerRock;
        public CircleCollider2D rockCollider;
        public Rigidbody2D rigidbody;
    }
}
