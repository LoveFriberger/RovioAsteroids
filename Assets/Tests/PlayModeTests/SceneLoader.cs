using Zenject;
using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

[TestFixture]
public class SceneLoader : ZenjectIntegrationTestFixture
{
    public void Install()
    {
        PreInstall();
        Container.Bind<SceneLoadingModel>().AsSingle();
        Container.Bind<SceneLoadingController.Settings>().AsSingle();
        Container.Bind<SceneLoadingController>().AsSingle();
        PostInstall();
    }

    [Inject]
    SceneLoadingController sceneLoadingController;
    [Inject]
    SceneLoadingController.Settings sceneLoadingControllerSettings;

    public IEnumerator LoadMainMenu()
    {
        Install();
        sceneLoadingControllerSettings.mainMenuKey = "Assets/Scenes/MainMenu.unity";
        yield return sceneLoadingController.ChangeSceneToMainMenu();
        Assert.That(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu");
        UnityEngine.SceneManagement.SceneManager.LoadScene("", UnityEngine.SceneManagement.LoadSceneMode.Single);
        yield break;
    }

    
    public IEnumerator LoadLevel()
    {
        Install();
        sceneLoadingControllerSettings.levelKey = "Assets/Scenes/Level.unity";
        yield return sceneLoadingController.ChangeSceneToLevel();
        Assert.That(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Level");
        sceneLoadingController.ChangeSceneToMainMenu();
        UnityEngine.SceneManagement.SceneManager.LoadScene("", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    
}
