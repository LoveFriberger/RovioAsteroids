using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : Manager
{
    AsyncOperationHandle<SceneInstance> loadedSceneHandle;

    public IEnumerator ChangeScene(string key)
    {
        var oldLoadedSceneHandle = loadedSceneHandle;

        loadedSceneHandle = Addressables.LoadSceneAsync(key, LoadSceneMode.Additive);

        yield return loadedSceneHandle;


        if (oldLoadedSceneHandle.IsValid())
            Addressables.UnloadSceneAsync(oldLoadedSceneHandle);

        if (loadedSceneHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(key + " failed to load!");
        }
        else if (loadedSceneHandle.Status == AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(loadedSceneHandle.Result.Scene);
        }
    }

}
