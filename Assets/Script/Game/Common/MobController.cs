using UnityEngine;

/// <summary>
/// A custom physics controller for a mob that deals with
/// on-frame collision with BoxCollider2D's. 
/// </summary>
public class MobController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The collider of this entity. This is what is checked against other colliders.")]
    private BoxCollider2D boxCollider = null;

    [SerializeField]
    [Tooltip("The collision marker of this entity. This determines how it'll interact with other colliders.")]
    private CollisionMarker collisionMarker = null;

    [SerializeField]
    [Tooltip("Speed data to load when initialized.")]
    private MobSpeedData speedData = null;

    [ReadOnly]
    [SerializeField]
    private float _speed = 1.0f;                // In units per second

    [ReadOnly]
    [SerializeField]
    private float _runningMultiplier = 2.0f;    // A percent as a decimal

    [ReadOnly]
    [SerializeField]
    private bool _running = false;

    [HideInInspector]
    public bool Running { get { return _running; } }

    [SerializeField]
    [ReadOnly]
    private Vector2 _direction = Vector2.zero;

    [HideInInspector]
    public Vector2 Direction { get { return _direction; } }

    [SerializeField]
    [ReadOnly]
    private bool _movementLocked = false;

    [HideInInspector]
    public bool MovementLocked { get { return _movementLocked; } }


    private void Awake()
    {
        ResetSettings();
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
        _direction = direction.normalized;
    }

    public void ResetSettings()
    {
        _speed = speedData.speed;
        _runningMultiplier = speedData.runningMultiplier;
        _running = false;
        _direction = Vector2.zero;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetRunning(bool running)
    {
        _running = running;
    }

    public void SetRunningMultiplier(float runningMultiplier)
    {
        _runningMultiplier = runningMultiplier;
    }

    /// <summary>
    /// If set to true, prevents the mobController from moving
    /// when called with UpdateFrame().
    /// </summary>
    public void SetMovementLock(bool locked)
    {
        _movementLocked = locked;
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
                        transform.position += (Vector3)(colliderDistance.pointA - colliderDistance.pointB);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Updates the controller by a single frame. Delta time used
    /// must be passed in to function.
    /// </summary>
    public void UpdateFrame(float deltaTime)
    {
        Vector3 calculatedVelocity = (Vector3)Direction * _speed * deltaTime;

        if(_running)
        {
            calculatedVelocity *= _runningMultiplier;
        }

        if(!MovementLocked)
        {
            transform.position += calculatedVelocity;
        }

        CalculateCollidedPosition();
    }
}
