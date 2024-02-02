using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneLoaderButton : MonoBehaviour
{
    [Inject]
    SceneLoadingManager sceneLoadingManager = null;

    [SerializeField]
    string sceneKey = "";
    public void LoadScene()
    {
        StartCoroutine(sceneLoadingManager.ChangeScene(sceneKey));
    }
}
