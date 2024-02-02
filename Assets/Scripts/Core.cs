using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField]
    string startSceneKey = "";

    public static bool IsLoaded { get; private set; }
    public static Dictionary<System.Type, Manager> ManagerMap = new Dictionary<System.Type, Manager>();
    void Awake()
    {
        LoadManagers();
        StartCoroutine(Get<SceneLoadingManager>().ChangeScene(startSceneKey));
    }

    void LoadManagers()
    {
        foreach (var manager in GetComponents<Manager>())
        {
            ManagerMap.Add(manager.GetType(), manager);
        }
        IsLoaded = true;
    }

    public static T Get<T>() where T : Manager
    {;
        if (ManagerMap.TryGetValue(typeof(T), out Manager manager))
            return manager as T;
        return null;
    }
}
