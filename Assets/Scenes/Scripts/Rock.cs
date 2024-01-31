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

    void OnEnable()
    {
        if(initSpeed > 0)
            GetComponent<Rigidbody2D>().velocity = transform.up * initSpeed;
    }

    public void TakeDamage()
    {
        GivePoints();

        Destroy(gameObject);

        if (smallerRock != null)
        {
            var rockRigidbody = GetComponent<Rigidbody2D>();

            var firstSpawnPosition = transform.position + transform.right * GetComponent<CircleCollider2D>().radius;
            var secondSpawnPosition = transform.position - transform.right * GetComponent<CircleCollider2D>().radius;

            var firstSmallerRock = Instantiate(smallerRock, firstSpawnPosition, transform.rotation);
            var secondSmallerRock = Instantiate(smallerRock, secondSpawnPosition, transform.rotation);

            firstSmallerRock.GetComponent<Rigidbody2D>().velocity = rockRigidbody.velocity + (Vector2)transform.right * splitSpeed;
            secondSmallerRock.GetComponent<Rigidbody2D>().velocity = rockRigidbody.velocity - (Vector2)transform.right * splitSpeed;
        }
    }

    void GivePoints()
    {

    }
}
