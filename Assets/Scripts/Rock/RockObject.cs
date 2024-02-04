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

    [Inject]
    public void Construct(RockDamageTaker rockDamageTaker, RockMover rockMover)
    {
        this.rockDamageTaker = rockDamageTaker;
        this.rockMover = rockMover;
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
}
