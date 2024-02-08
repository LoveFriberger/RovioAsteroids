using System.Collections;
using System;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;

public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    [SerializeField]
    GameSettings gameSettings;
    [SerializeField]
    PlayerSettings playerSettings;
    [SerializeField]
    LevelSettings levelSettings;
    [SerializeField]
    RockSettings rockSettings;
    [SerializeField]
    UISettings uiSettings;

    [Serializable]
    public class GameSettings
    {
        public SceneLoadingController.Settings sceneLoaderSettings;
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMover.Settings playerMoverSettings;
        public PlayerShooter.Settings playerShooterSettings;
        public Projectile.Settings projectileSettings;
    }

    [Serializable]
    public class LevelSettings
    {
        public PlayerSpawner.Settings playerSpawnerSettings;
        public RockSpawner.Settings rockSpawnerSettings;
    }

    [Serializable]
    public class UISettings
    {
        public GameMenuTitleText.Settings gameMenuTitleTextSettings;
    }

    [Serializable]
    public class RockSettings
    {
        public RockMover.Settings rockMoverSettings;
        public RockDamageTaker.Settings rockDamageTakerSettings;
    }

    public override void InstallBindings()
    {
        Container.BindInstances(gameSettings.sceneLoaderSettings);
        Container.BindInstances(playerSettings.playerMoverSettings);
        Container.BindInstances(playerSettings.playerShooterSettings);
        Container.BindInstances(playerSettings.projectileSettings);
        Container.BindInstances(levelSettings.playerSpawnerSettings);
        Container.BindInstances(levelSettings.rockSpawnerSettings);
        Container.BindInstances(uiSettings.gameMenuTitleTextSettings);
        Container.BindInstances(rockSettings.rockMoverSettings);
        Container.BindInstances(rockSettings.rockDamageTakerSettings);
    }
}
