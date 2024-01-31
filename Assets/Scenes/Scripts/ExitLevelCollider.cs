using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelCollider : MonoBehaviour
{
    [SerializeField]
    Camera levelCamera = null;

    [SerializeField]
    BoxCollider2D boxCollider = null;

    private void OnEnable()
    {
        var cameraOrthographicWidth = levelCamera.orthographicSize * levelCamera.aspect;
        boxCollider.size = new Vector2 (cameraOrthographicWidth, levelCamera.orthographicSize) * 2;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        var velocity = collision.GetComponent<Rigidbody2D>().velocity;
        if (collision.bounds.max.x > boxCollider.bounds.max.x && velocity.x > 0)
            TeleportXAxisPosition(collision, false);
        if (collision.bounds.min.x < boxCollider.bounds.min.x && velocity.x < 0)
            TeleportXAxisPosition(collision, true);
        if (collision.bounds.max.y > boxCollider.bounds.max.y && velocity.y > 0)
            TeleportYAxisPosition(collision, false);
        if (collision.bounds.min.y < boxCollider.bounds.min.y && velocity.y < 0)
            TeleportYAxisPosition(collision, true);

    }
    
    void TeleportXAxisPosition(Collider2D collision, bool leftToRight)
    {
        var oldXValue = collision.transform.position.x;
        var collisionBoundsEdgeValue = leftToRight ? collision.bounds.min.x : collision.bounds.max.x;
        var collisionBoundingBoxOffset = oldXValue - collisionBoundsEdgeValue;

        var boxColliderBoundsEdgeValue = leftToRight ? boxCollider.bounds.max.x : boxCollider.bounds.min.x;
        collision.transform.position = new Vector2(boxColliderBoundsEdgeValue + collisionBoundingBoxOffset, collision.transform.position.y);
    }

    void TeleportYAxisPosition(Collider2D collision, bool bottomToTop)
    {
        var oldYValue = collision.transform.position.y;
        var collisionBoundsEdgeValue = bottomToTop ? collision.bounds.min.y : collision.bounds.max.y;
        var collisionBoundingBoxOffset = oldYValue - collisionBoundsEdgeValue;

        var boxColliderBoundsEdgeValue = bottomToTop ? boxCollider.bounds.max.y : boxCollider.bounds.min.y;
        collision.transform.position = new Vector2(collision.transform.position.x, boxColliderBoundsEdgeValue + collisionBoundingBoxOffset);
    }
}
