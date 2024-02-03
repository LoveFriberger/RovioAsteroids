using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerObject : MonoBehaviour
{
    PlayerModel player = null;

    [Inject]
    public void Construct(PlayerModel player)
    {
        this.player = player;
    }
}
