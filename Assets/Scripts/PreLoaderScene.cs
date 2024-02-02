using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PreLoaderScene : MonoBehaviour
{
    [SerializeField]
    string startSceneKey = "";

    [Inject]
    SceneLoadingController sceneLoadingController;
    void Awake()
    {
        StartCoroutine(sceneLoadingController.ChangeScene(startSceneKey));
    }
}
