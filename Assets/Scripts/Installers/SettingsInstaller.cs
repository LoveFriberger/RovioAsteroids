using System.Collections;
using System;
using UnityEngine;
using Zenject;

public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    [SerializeField]
    PlayerSettings playerSettings = null;

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMover.Settings playerMoverSettings = null;
        public PlayerShooter.Settings playerShooterSettings = null;
    }

    public override void InstallBindings()
    {
        Container.BindInstances(playerSettings.playerMoverSettings);
        Container.BindInstances(playerSettings.playerShooterSettings);
    }
}
