using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IHitable
{
    [Inject]
    GameManager gameManager = null;

    public void TakeDamage()
    {
        Core.Get<GameManager>().PlayerKilled?.Invoke();
    }
}
