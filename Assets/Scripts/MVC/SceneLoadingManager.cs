using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadingModel
{
    public AsyncOperationHandle<SceneInstance> loadedSceneHandle;
}

public class SceneLoadingViewer
{
    protected SceneLoadingModel model = new();

    public SceneLoadingViewer() { }
}

public class SceneLoadingController: SceneLoadingViewer
{
    readonly Settings settings = null;
    
    public SceneLoadingController(Settings settings, InputView inputView) 
    {
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
