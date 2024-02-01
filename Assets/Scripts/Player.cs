using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHitable
{
    public void TakeDamage()
    {
        var gameManager = Core.Get<GameManager>();
        gameManager.PlayerKilled?.Invoke();
    }
}
