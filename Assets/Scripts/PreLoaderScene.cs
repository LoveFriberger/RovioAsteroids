using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreLoaderScene : MonoBehaviour
{
    [SerializeField]
    string startSceneKey = "";

    void Awake()
    {
        StartCoroutine(Core.Get<SceneLoadingManager>().ChangeScene(startSceneKey));
    }
}
