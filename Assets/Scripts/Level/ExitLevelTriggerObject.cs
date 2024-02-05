using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider2D))]
public class ExitLevelTriggerObject : MonoBehaviour
{
    ExitLevelTrigger exitLevelTrigger = null;

    [Inject]
    public void Construct(ExitLevelTrigger exitLevelTrigger)
    {
        this.exitLevelTrigger = exitLevelTrigger;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        exitLevelTrigger.OnTriggerExit2D(collision);
    }
}
