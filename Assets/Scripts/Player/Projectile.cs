using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float lifeTime = 2;

    float startTime = 0;
    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (startTime + lifeTime < Time.time)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null)
            hitable.TakeDamage();

        Destroy(gameObject);
    }

}
