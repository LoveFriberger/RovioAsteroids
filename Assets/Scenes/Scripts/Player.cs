using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHitable
{
    public void TakeDamage()
    {
        Debug.Log("Killed!");
    }
}
