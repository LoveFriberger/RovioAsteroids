using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject; 
public class LevelObject : MonoBehaviour
{
    LevelModel level = null;

    [Inject]
    public void Construct(LevelModel level)
    {
        this.level = level;
    }
}
