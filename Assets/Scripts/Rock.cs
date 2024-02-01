using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IHitable
{
    [SerializeField]
    Rock smallerRock = null;
    [SerializeField]
    float splitSpeed = 0.5f;
    [SerializeField]
    float initSpeed = 0;
    [SerializeField]
    CircleCollider2D circleCollider = null;
    [SerializeField]
    Rigidbody2D rockRigidbody;
    public Rigidbody2D RockRigidbody => rockRigidbody;

    void OnEnable()
    {
        //The big rocks are the only ones that have a initial speed.
        if(initSpeed > 0)
            RockRigidbody.velocity = transform.up * initSpeed;
    }

    public void TakeDamage()
    {
        GivePoints();

        Destroy(gameObject);

        if (smallerRock != null)
        { 
            var firstSpawnPosition = transform.position + transform.right * circleCollider.radius;
            var secondSpawnPosition = transform.position - transform.right * circleCollider.radius;

            var firstSmallerRock = Instantiate(smallerRock, firstSpawnPosition, transform.rotation);
            var secondSmallerRock = Instantiate(smallerRock, secondSpawnPosition, transform.rotation);

            firstSmallerRock.RockRigidbody.velocity = RockRigidbody.velocity + (Vector2)transform.right * splitSpeed;
            secondSmallerRock.RockRigidbody.velocity = RockRigidbody.velocity - (Vector2)transform.right * splitSpeed;
        }
    }

    void GivePoints()
    {
        Core.Get<PointsManager>().AddPoints(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null)
            hitable.TakeDamage();
    }
}
