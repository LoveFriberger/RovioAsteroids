using UnityEngine;
using Zenject;
using System;

public class Projectile : MonoBehaviour
{
    float lifeTime = 0;
    float startTime = 0;

    [Inject]
    public void Construct(Settings settings)
    {
        lifeTime = settings.lifeTime;
        startTime = Time.time;
    }

    void Update()
    {
        if (startTime + lifeTime < Time.time)
            DestroyProjectile();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var hitable = collision.gameObject.GetComponent<IHitable>();
        if (hitable != null)
            hitable.TakeDamage();

        DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Debug.Log("Destroying projectile");
        Destroy(gameObject);
    }

    [Serializable]
    public class Settings
    {
        public float lifeTime = 2;
    }
}
