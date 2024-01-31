using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    float cooldown = 0.5f;
    [SerializeField]
    Transform projectileStartTransform = null;
    [SerializeField]
    GameObject projectilePrefab = null;
    [SerializeField]
    float projectileVelocity = 10;

    float timeLastShot = 0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            Shoot();
    }

    void Shoot()
    {
        if (timeLastShot + cooldown > Time.time)
            return;

        var projectileClone = Instantiate(projectilePrefab, projectileStartTransform.position, projectileStartTransform.rotation);
        projectileClone.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity + (Vector2)projectileClone.transform.up * projectileVelocity;
        timeLastShot = Time.time;
    }
}
