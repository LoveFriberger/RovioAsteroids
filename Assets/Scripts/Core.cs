using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    static Dictionary<System.Type, Manager> ManagerMap = new Dictionary<System.Type, Manager>();
    public static bool IsLoaded { get; private set; }
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        LoadManagers();
        IsLoaded = true;
    }

    void LoadManagers()
    {
        foreach (var manager in GetComponents<Manager>())
        {
            ManagerMap.Add(manager.GetType(), manager);
        }
    }

    public static T Get<T>() where T : Manager
    {
        Manager manager = null;
        if (ManagerMap.TryGetValue(typeof(T), out manager))
            return manager as T;
        return null;
    }
}
