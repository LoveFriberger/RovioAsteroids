using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class RockObject : MonoBehaviour, IHitable
{
    RockDamageTaker rockDamageTaker = null;
    RockMover rockMover = null;
    RockSpawner rockSpawner = null;
    GameController gameController = null;

    [Inject]
    public void Construct(RockDamageTaker rockDamageTaker, RockMover rockMover, RockSpawner rockSpawner, GameController gameController)
    {
        this.rockDamageTaker = rockDamageTaker;
        this.rockMover = rockMover;
        this.rockSpawner = rockSpawner;
        this.gameController = gameController;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null)
            hitable.TakeDamage();
    }

    public void TakeDamage()
    {
        rockDamageTaker.TakeDamage();
        Destroy(gameObject);
    }

    public void AddVelocity(Vector2 velocity)
    {
        rockMover.AddVelocity(velocity);
    }

    private void OnDestroy()
    {
        rockSpawner.RemoveOnResetAction();
    }
}
