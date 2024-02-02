using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField]
    Rock rockPrefab = null;

    public override void InstallBindings()
    {
        Container.BindFactory<Rock, Rock.Factory>().FromComponentInNewPrefab(rockPrefab);
    }
}
