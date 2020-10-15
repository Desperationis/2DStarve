using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A custom physics controller for a mob. 
/// </summary>
public class MobController : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private CollisionMarker collisionMarker;

    [SerializeField]
    [Tooltip("The number of units passed per second.")]
    private float speed = 5.0f;

    [SerializeField]
    private Vector2 _velocity = Vector2.zero;

    public Vector2 Velocity { get { return _velocity; } }

    /// <summary>
    /// Sets the direction the mob controller will drive the mob to 
    /// when called with UpdateFrame(). 
    /// </summary>
    /// <param name="direction">
    /// A vector that represents the direction of the mob controller.
    /// This will be normalized automatically. 
    /// </param>
    public void SetDirection(Vector2 direction)
    {
        _velocity = direction.normalized * speed;
    }

    /// <summary>
    /// Force the mob controller to take a certain velocity.
    /// </summary>
    /// <param name="velocity"></param>
    public void ForceVelocity(Vector2 velocity)
    {
        _velocity = velocity;
    }

    /// <summary>
    /// Updates the position of the entity based on any colliding 
    /// BoxCollider2D's. 
    /// </summary>
    public void CalculateCollidedPosition()
    {
        Collider2D[] colliderHits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0.0f);

        foreach (Collider2D hit in colliderHits)
        {
            if (hit == boxCollider) continue;

            CollisionMarker hitMarker = hit.GetComponent<CollisionMarker>();

            if (collisionMarker.IsPushed(hitMarker.CollisionNumber))
            {
                ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

                if (colliderDistance.isOverlapped)
                {
                    transform.position += (Vector3)(colliderDistance.pointA - colliderDistance.pointB);
                }
            }
        }
    }

    /// <summary>
    /// Updates the controller by a single frame.
    /// </summary>
    public void UpdateFrame()
    {
        transform.position += (Vector3)_velocity * BoltNetwork.FrameDeltaTime;
        CalculateCollidedPosition();
    }
}
