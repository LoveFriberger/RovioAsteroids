using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    Core corePrefab = null;
    [SerializeField]
    string highScorePlayerPrefsKey = "HighScore";

    public override void InstallBindings()
    {
        if (!Core.IsLoaded)
        {
            var core = Instantiate(corePrefab);
            DontDestroyOnLoad(core);
        }

        Container.BindInstance(new PointsController(highScorePlayerPrefsKey));
        Container.Bind<SceneLoadingManager>().FromInstance(Core.Get<SceneLoadingManager>());
        Container.Bind<GameManager>().FromInstance(Core.Get<GameManager>());
    }
}
