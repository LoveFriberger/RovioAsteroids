using System.Collections;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    [SerializeField]
    PlayerSettings playerSettings = null;
    [SerializeField]
    LevelSettings levelSettings = null;
    [SerializeField]
    RockSettings rockSettings = null;

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMover.Settings playerMoverSettings = null;
        public PlayerShooter.Settings playerShooterSettings = null;
    }

    [Serializable]
    public class LevelSettings
    {
        public PlayerSpawner.Settings playerSpawnerSettings = null;
        public RockSpawner.Settings rockSpawnerSettings = null;
    }

    [Serializable]
    public class RockSettings
    {
        public RockMover.Settings rockMoverSettings = null;
        public RockDamageTaker.Settings rockDamageTakerSettings = null;
    }

    public override void InstallBindings()
    {
        Container.BindInstances(playerSettings.playerMoverSettings);
        Container.BindInstances(playerSettings.playerShooterSettings);
        Container.BindInstances(levelSettings.playerSpawnerSettings);
        Container.BindInstances(levelSettings.rockSpawnerSettings);
        Container.BindInstances(rockSettings.rockMoverSettings);
        Container.BindInstances(rockSettings.rockDamageTakerSettings);
    }
}
