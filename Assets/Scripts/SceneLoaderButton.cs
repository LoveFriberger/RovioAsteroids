using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderButton : MonoBehaviour
{

    [SerializeField]
    string sceneKey = "";
    public void LoadScene()
    {
        var sceneManager = Core.Get<SceneLoadingManager>();
        StartCoroutine(sceneManager.ChangeScene(sceneKey));
    }
}
