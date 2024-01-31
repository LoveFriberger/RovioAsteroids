using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField]
    float cooldown = 0.5f;
    [SerializeField]
    Transform projectileStartTransform = null;
    [SerializeField]
    float projectileVelocity = 10;

    [SerializeField]
    AssetReferenceGameObject projectilePrefabReference = null;

    float timeLastShot = 0f;
    bool actionInputValue = false;

    void Start()
    {
        var handle = projectilePrefabReference.LoadAssetAsync<GameObject>();
        handle.Completed += HandleComplete;
    }

    void HandleComplete(AsyncOperationHandle<GameObject> operationHandle)
    {
        if (operationHandle.Status != AsyncOperationStatus.Succeeded)
            Debug.LogError(projectilePrefabReference.RuntimeKey + " failed to load!");
    }

    public void OnActionInput(InputAction.CallbackContext context)
    {
        actionInputValue = context.ReadValueAsButton();
    }

    void FixedUpdate()
    {
        if (actionInputValue)
            Shoot();
    }

    void Shoot()
    {
        if (timeLastShot + cooldown > Time.time)
            return;

        var projectileClone = ((GameObject)Instantiate(projectilePrefabReference.Asset, projectileStartTransform.position, projectileStartTransform.rotation)).GetComponent<Projectile>();
        projectileClone.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Rigidbody2D>().velocity + (Vector2)projectileClone.transform.up * projectileVelocity;
        timeLastShot = Time.time;
    }

    private void OnDestroy()
    {
        projectilePrefabReference.ReleaseAsset();
    }
}
