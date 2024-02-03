using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public SceneLoadingController() { }

    public IEnumerator ChangeScene(string key)
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

}
