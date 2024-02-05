using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExitLevelTrigger : IInitializable
{
    BoxCollider2D exitLevelBoxCollider = null;

    public ExitLevelTrigger([Inject(Id ="exitLevelCollider")] BoxCollider2D exitLevelBoxCollider)
    {
        this.exitLevelBoxCollider = exitLevelBoxCollider;
    }

    public void Initialize()
    {
        SetColliderToCameraFrameSize();
    }

    void SetColliderToCameraFrameSize()
    {
        var cameraOrthographicWidth = Camera.main.orthographicSize * Camera.main.aspect;
        exitLevelBoxCollider.size = new Vector2(cameraOrthographicWidth, Camera.main.orthographicSize) * 2;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        var velocity = collision.GetComponent<Rigidbody2D>().velocity;
        if (collision.bounds.max.x > exitLevelBoxCollider.bounds.max.x && velocity.x > 0)
            TeleportXAxisPosition(collision, false);
        if (collision.bounds.min.x < exitLevelBoxCollider.bounds.min.x && velocity.x < 0)
            TeleportXAxisPosition(collision, true);
        if (collision.bounds.max.y > exitLevelBoxCollider.bounds.max.y && velocity.y > 0)
            TeleportYAxisPosition(collision, false);
        if (collision.bounds.min.y < exitLevelBoxCollider.bounds.min.y && velocity.y < 0)
            TeleportYAxisPosition(collision, true);

    }
    
    void TeleportXAxisPosition(Collider2D collision, bool leftToRight)
    {
        var oldXValue = collision.transform.position.x;
        var collisionBoundsEdgeValue = leftToRight ? collision.bounds.min.x : collision.bounds.max.x;
        var collisionBoundingBoxOffset = oldXValue - collisionBoundsEdgeValue;

        var boxColliderBoundsEdgeValue = leftToRight ? exitLevelBoxCollider.bounds.max.x : exitLevelBoxCollider.bounds.min.x;
        collision.transform.position = new Vector2(boxColliderBoundsEdgeValue + collisionBoundingBoxOffset, collision.transform.position.y);
    }

    void TeleportYAxisPosition(Collider2D collision, bool bottomToTop)
    {
        var oldYValue = collision.transform.position.y;
        var collisionBoundsEdgeValue = bottomToTop ? collision.bounds.min.y : collision.bounds.max.y;
        var collisionBoundingBoxOffset = oldYValue - collisionBoundsEdgeValue;

        var boxColliderBoundsEdgeValue = bottomToTop ? exitLevelBoxCollider.bounds.max.y : exitLevelBoxCollider.bounds.min.y;
        collision.transform.position = new Vector2(collision.transform.position.x, boxColliderBoundsEdgeValue + collisionBoundingBoxOffset);
    }
}
