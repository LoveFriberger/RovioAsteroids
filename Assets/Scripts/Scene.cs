using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Scene : MonoBehaviour
{
    [SerializeField]
    Core corePrefab = null;

    void Awake()
    {
        if (!Core.IsLoaded)
            Instantiate(corePrefab);

        Setup();
    }

    protected abstract void Setup();
}
