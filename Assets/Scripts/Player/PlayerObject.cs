using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerObject : MonoBehaviour, IHitable
{
    PlayerModel player = null;
    PlayerDamageTaker playerDamageTaker = null;

    [Inject]
    public void Construct(PlayerModel player, PlayerDamageTaker playerDamageTaker)
    {
        this.player = player;
        this.playerDamageTaker = playerDamageTaker;
    }

    public void TakeDamage()
    {
        playerDamageTaker.TakeDamage();
    }
}
