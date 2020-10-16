using UnityEngine;

/// <summary>
/// A custom physics controller for a mob that deals with
/// on-frame collision with BoxCollider2D's. 
/// </summary>
public class MobController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The collider of this entity. This is what is checked against other colliders.")]
    private BoxCollider2D boxCollider;

    [SerializeField]
    [Tooltip("The collision marker of this entity. This determines how it'll interact with other colliders.")]
    private CollisionMarker collisionMarker;

    [SerializeField]
    [Tooltip("The walking speed of the entity in units per second.")]
    private float speed = 5.0f;

    [SerializeField]
    [Tooltip("How many times faster running is than walking.")]
    private float runningMultiplier = 1.5f;

    [ReadOnly]
    [SerializeField]
    private bool isRunning = false;

    [SerializeField]
    [ReadOnly]
    private Vector2 _direction = Vector2.zero;
    public Vector2 Direction { get { return _direction; } }

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
        _direction = direction.normalized;
    }


    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetRunning(bool running)
    {
        isRunning = running;
    }

    public void SetRunningMultiplier(float runningMultiplier)
    {
        this.runningMultiplier = runningMultiplier;
    }


    /// <summary>
    /// Updates the position of the entity based on any colliding 
    /// BoxCollider2D's and CollisionMarker
    /// </summary>
    public void CalculateCollidedPosition()
    {
        Collider2D[] colliderHits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0.0f);

        foreach (Collider2D hit in colliderHits)
        {
            if (hit == boxCollider) continue;

            // Determine if this entity gets pushed by other colliders
            // or phases through them
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
    /// Updates the controller by a single frame. This function MUST be called
    /// in FixedUpdate() or some other variant of it to work. 
    /// </summary>
    public void UpdateFixedFrame()
    {
        Vector3 calculatedVelocity = (Vector3)Direction * speed * BoltNetwork.FrameDeltaTime;

        if(isRunning)
        {
            calculatedVelocity *= runningMultiplier;
        }

        transform.position += calculatedVelocity;
        CalculateCollidedPosition();
    }
}
