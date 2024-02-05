using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PreLoaderScene : MonoBehaviour
{
    [Inject]
    SceneLoadingController sceneLoadingController;
    void Awake()
    {
        StartCoroutine(sceneLoadingController.ChangeSceneToMainMenu());
    }
}
