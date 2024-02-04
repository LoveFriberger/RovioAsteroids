using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTaker : IHitable
{
    readonly GameController gameController = null;

    public PlayerDamageTaker(GameController gameController)
    {
        this.gameController = gameController;
    }
        
    public void TakeDamage()
    {
        gameController.InvokePlayerKilledAction();
    }
}
