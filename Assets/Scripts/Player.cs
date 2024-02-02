using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour, IHitable
{
    [Inject]
    GameController gameController = null;

    public void TakeDamage()
    {
        gameController.InvokePlayerKilledAction();
    }

    public class Factory : PlaceholderFactory<Player> { }
}
