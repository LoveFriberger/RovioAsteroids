using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;


public class SceneLoadingController
{
    readonly SceneLoadingModel model;
    readonly Settings settings;
    
    public SceneLoadingController(SceneLoadingModel model, Settings settings) 
    {
        this.model = model;
        this.settings = settings;
    }

    IEnumerator ChangeScene(string key)
    {
        Debug.Log(string.Format("Starting scene change to {0}", key));
        var oldLoadedSceneHandle = model.loadedSceneHandle;
        var loadMode = oldLoadedSceneHandle.IsValid() ? LoadSceneMode.Additive : LoadSceneMode.Single;
        model.loadedSceneHandle = Addressables.LoadSceneAsync(key, loadMode);
        yield return model.loadedSceneHandle;

        if (oldLoadedSceneHandle.IsValid())
        {
            Debug.Log(string.Format("Unloading scene {0}", oldLoadedSceneHandle.DebugName));
            Addressables.UnloadSceneAsync(oldLoadedSceneHandle);
        }

        if (model.loadedSceneHandle.Status == AsyncOperationStatus.Failed)
            Debug.LogError(string.Format("{0} failed to load!", key));

        else if (model.loadedSceneHandle.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(model.loadedSceneHandle.Result.Scene);
            Debug.Log(string.Format("Loaded scene {0}", key));
        }
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
