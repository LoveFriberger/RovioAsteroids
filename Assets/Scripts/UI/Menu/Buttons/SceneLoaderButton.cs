using UnityEngine;
using Zenject;

public class SceneLoaderButton : MonoBehaviour
{
    public enum Scene
    {
        MainMenu,
        Level
    }

    [Inject]
    SceneLoadingController sceneLoadingController;

    [SerializeField]
    Scene sceneToChangeTo = Scene.MainMenu;

    public void LoadScene()
    {
        switch (sceneToChangeTo)
        {
            case Scene.MainMenu:
                StartCoroutine(sceneLoadingController.ChangeSceneToMainMenu());
                break;
            case Scene.Level:
                StartCoroutine(sceneLoadingController.ChangeSceneToLevel());
                break;
        }
    }
}
