using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageTaker : IHitable
{
    readonly GameManagerController gameController = null;

    public PlayerDamageTaker(GameManagerController gameController)
    {
        this.gameController = gameController;
    }
        
    public void TakeDamage()
    {
        gameController.InvokePlayerKilledAction();
    }
}
