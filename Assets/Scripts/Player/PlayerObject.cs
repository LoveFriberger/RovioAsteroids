using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerObject : MonoBehaviour, IHitable
{
    PlayerDamageTaker playerDamageTaker;

    [Inject]
    public void Construct(PlayerDamageTaker playerDamageTaker)
    {
        this.playerDamageTaker = playerDamageTaker;
    }

    public void TakeDamage()
    {
        playerDamageTaker.TakeDamage();
    }
}
