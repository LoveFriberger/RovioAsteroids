using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class SceneLoadingController
{
    readonly SceneLoadingModel model = null;
    readonly Settings settings = null;
    
    public SceneLoadingController(SceneLoadingModel model, Settings settings) 
    {
        this.model = model;
        this.settings = settings;
    }

    IEnumerator ChangeScene(string key)
    {
        var oldLoadedSceneHandle = model.loadedSceneHandle;
        var loadMode = oldLoadedSceneHandle.IsValid() ? LoadSceneMode.Additive : LoadSceneMode.Single;
        model.loadedSceneHandle = Addressables.LoadSceneAsync(key, loadMode);
        yield return model.loadedSceneHandle;

        if (oldLoadedSceneHandle.IsValid())
            Addressables.UnloadSceneAsync(oldLoadedSceneHandle);

        if (model.loadedSceneHandle.Status == AsyncOperationStatus.Failed)
            Debug.LogError(key + " failed to load!");

        else if (model.loadedSceneHandle.Status == AsyncOperationStatus.Succeeded)
            SceneManager.SetActiveScene(model.loadedSceneHandle.Result.Scene);
    }

    public IEnumerator ChangeSceneToLevel()
    {
        yield return ChangeScene(settings.levelKey);
    }

    public IEnumerator ChangeSceneToMainMenu()
    {
        yield return ChangeScene(settings.mainMenuKey);
    }

    [Serializable]
    public class Settings
    {
        public string mainMenuKey;
        public string levelKey;
    }
}
