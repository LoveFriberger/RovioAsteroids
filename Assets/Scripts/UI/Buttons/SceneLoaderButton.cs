using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneLoaderButton : MonoBehaviour
{
    [Inject]
    SceneLoadingController sceneLoadingController = null;

    [SerializeField]
    string sceneKey = "";
    public void LoadScene()
    {
        StartCoroutine(sceneLoadingController.ChangeScene(sceneKey));
    }
}
