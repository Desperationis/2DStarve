using UnityEngine;

/// <summary>
/// A custom physics controller for collisions with BoxCollider2Ds. 
/// This is the script solely responsible for mob movement and direction.
/// </summary>
public class MobController : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D boxCollider = null;

    [SerializeField]
    public CollisionMarker collisionMarker = null;

    public float speed { get; private set; }
    public float runningMultiplier { get; private set; }
    public bool isRunning { get; private set; }
    public Vector2 direction { get; private set; }
    public bool movementDisabled { get; private set; }

    public Vector2 _cardinalDirection = Vector2.down;

    public Vector2 cardinalDirection
    {
        // Returns the cardinal direction of the mob controller as 
        // a normalized vector in only the 4 major directions.
        // If not moving, return the last known cardinal direction.
        get
        {
            if (direction != Vector2.zero)
            {
                // Get the cardinal direction of the greater x or y component
                Vector2 newCardinalDirection = direction;
                bool xComponentGreater = Mathf.Abs(direction.x) >= Mathf.Abs(direction.y);
                newCardinalDirection.x = xComponentGreater ? Mathf.Sign(direction.x) : 0;
                newCardinalDirection.y = !xComponentGreater ? Mathf.Sign(direction.y) : 0;

                _cardinalDirection = newCardinalDirection;
            }

            return _cardinalDirection;
        }
    }

    public bool isMoving { 
        get
        {
            return direction.sqrMagnitude != 0.0f && !movementDisabled;
        } 
    }

    private void Awake()
    {
        EnableMovement();
        SetSpeed(1);
        SetRunningMultiplier(1);
        SetRunning(false);
        Stop();
    }

    public void DisableMovement()
    {
        movementDisabled = true;
    }

    public void EnableMovement()
    {
        movementDisabled = false;
    }

    /// <summary>
    /// Stops the gameObject from moving.
    /// </summary>
    public void Stop()
    {
        isRunning = false;
        direction = Vector2.zero;
    }

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
        if(!movementDisabled)
        {
            this.direction = direction.normalized;
        }
    }


    /// <summary>
    /// Set the speed of the MobController in Unity Units per second.
    /// </summary>
    /// <param name="speed">A speed greater than or equal to 0.</param>
    public void SetSpeed(float speed)
    {
        if(speed >= 0.0f)
        {
            this.speed = speed;
        }
    }

    public void SetRunningMultiplier(float runningMultiplier)
    {
        if (runningMultiplier >= 1.0f)
        {
            this.runningMultiplier = runningMultiplier;
        }
    }

    /// <summary>
    /// Whether or not the current speed will be multiplied
    /// by the running multiplier.
    /// </summary>
    /// <param name="running"></param>
    public void SetRunning(bool running)
    {
        isRunning = running;
    }

    /// <summary>
    /// Calculate the position object this gameObject has to make to not 
    /// collide with any entities.
    /// </summary>
    private Vector2 CalculateCollisionOffset()
    {
        Collider2D[] colliderHits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0.0f);

        Vector2 offset = Vector2.zero;

        foreach (Collider2D hit in colliderHits)
        {
            if (hit == boxCollider) continue;

            // Phase through if hit marker is not found
            CollisionMarker hitMarker = hit.GetComponent<CollisionMarker>();

            if(hitMarker)
            {
                // Determine if this entity gets pushed by other colliders
                // or phases through them
                if (CollisionMarker.IsPushed(collisionMarker, hitMarker))
                {
                    ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

                    if (colliderDistance.isOverlapped)
                    {
                        offset += colliderDistance.pointA - colliderDistance.pointB;
                    }
                }
            }
        }

        return offset;
    }


    /// <summary>
    /// Updates the controller by a single frame based on a
    /// delta time.
    /// </summary>
    public void UpdateFrame(float deltaTime)
    {
        Vector3 calculatedVelocity = (Vector3)direction * speed * deltaTime;

        if(isRunning)
        {
            calculatedVelocity *= runningMultiplier;
        }
        
        if(!movementDisabled)
        {
            transform.position += calculatedVelocity;
        }

        transform.position += (Vector3)CalculateCollisionOffset();
    }
}
