using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ExitLevelCollider : MonoBehaviour
{
    [Inject]
    LevelModel levelModel = null;

    private void OnEnable()
    {
        SetColliderToCameraFrameSize();
    }

    void SetColliderToCameraFrameSize()
    {
        var cameraOrthographicWidth = Camera.main.orthographicSize * Camera.main.aspect;
        levelModel.ExitLevelColliderSize = new Vector2(cameraOrthographicWidth, Camera.main.orthographicSize) * 2;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var velocity = collision.GetComponent<Rigidbody2D>().velocity;
        if (collision.bounds.max.x > levelModel.ExitLevelColliderBounds.max.x && velocity.x > 0)
            TeleportXAxisPosition(collision, false);
        if (collision.bounds.min.x < levelModel.ExitLevelColliderBounds.min.x && velocity.x < 0)
            TeleportXAxisPosition(collision, true);
        if (collision.bounds.max.y > levelModel.ExitLevelColliderBounds.max.y && velocity.y > 0)
            TeleportYAxisPosition(collision, false);
        if (collision.bounds.min.y < levelModel.ExitLevelColliderBounds.min.y && velocity.y < 0)
            TeleportYAxisPosition(collision, true);

    }
    
    void TeleportXAxisPosition(Collider2D collision, bool leftToRight)
    {
        var oldXValue = collision.transform.position.x;
        var collisionBoundsEdgeValue = leftToRight ? collision.bounds.min.x : collision.bounds.max.x;
        var collisionBoundingBoxOffset = oldXValue - collisionBoundsEdgeValue;

        var boxColliderBoundsEdgeValue = leftToRight ? levelModel.ExitLevelColliderBounds.max.x : levelModel.ExitLevelColliderBounds.min.x;
        collision.transform.position = new Vector2(boxColliderBoundsEdgeValue + collisionBoundingBoxOffset, collision.transform.position.y);
    }

    void TeleportYAxisPosition(Collider2D collision, bool bottomToTop)
    {
        var oldYValue = collision.transform.position.y;
        var collisionBoundsEdgeValue = bottomToTop ? collision.bounds.min.y : collision.bounds.max.y;
        var collisionBoundingBoxOffset = oldYValue - collisionBoundsEdgeValue;

        var boxColliderBoundsEdgeValue = bottomToTop ? levelModel.ExitLevelColliderBounds.max.y : levelModel.ExitLevelColliderBounds.min.y;
        collision.transform.position = new Vector2(collision.transform.position.x, boxColliderBoundsEdgeValue + collisionBoundingBoxOffset);
    }
}
