using System.Collections;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

public class RockInstaller : MonoInstaller
{
    [SerializeField]
    RockObject rockObject = null;
    [SerializeField]
    Settings settings = null;

    public override void InstallBindings()
    {
        Container.Bind<RockModel>().AsSingle().WithArguments(settings);

        Container.Bind<RockDamageTaker>().AsSingle();
        Container.BindInterfacesAndSelfTo<RockMover>().AsSingle(); 

        Container.BindInstances(rockObject);
    }

    [Serializable]
    public class Settings
    {
        public AssetReferenceGameObject smallerRock = null;
        public CircleCollider2D rockCollider = null;
        public Rigidbody2D rigidbody = null;
    }
}
