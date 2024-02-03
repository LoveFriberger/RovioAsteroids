using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class Rock : MonoBehaviour, IHitable
{
    [SerializeField]
    Rock smallerRock = null;
    [SerializeField]
    float splitSpeed = 0.5f;
    [SerializeField]
    float initSpeed = 3;
    [SerializeField]
    CircleCollider2D circleCollider = null;
    [SerializeField]
    Rigidbody2D rockRigidbody;
    

    public Rigidbody2D RockRigidbody => rockRigidbody;

    [Inject]
    PointsController pointsController = null;

    void Start()
    {
        RockRigidbody.velocity += (Vector2)transform.up * initSpeed;
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

            firstSmallerRock.RockRigidbody.velocity = (Vector2)transform.right * splitSpeed;
            secondSmallerRock.RockRigidbody.velocity = -(Vector2)transform.right * splitSpeed;
        }
    }

    void GivePoints()
    {
        pointsController.AddPoints(1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null)
            hitable.TakeDamage();
    }
}
