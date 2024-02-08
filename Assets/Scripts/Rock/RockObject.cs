using UnityEngine;
using Zenject;

public class RockObject : MonoBehaviour, IHitable
{
    RockDamageTaker rockDamageTaker;
    RockMover rockMover;

    [Inject]
    public void Construct(RockDamageTaker rockDamageTaker, RockMover rockMover)
    {
        this.rockDamageTaker = rockDamageTaker;
        this.rockMover = rockMover;
    }

    void OnCollisionEnter2D(Collision2D collision)
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
