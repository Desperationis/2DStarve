using UnityEngine;

/// <summary>
/// Used to mark the collision number of an object or entity; In relation
/// to another entity's number:
/// 
/// Same number = No collision
/// Lower Number = Pushed by it
/// Higher Number = Is the pusher
/// 
/// </summary>
[DisallowMultipleComponent]
public class CollisionMarker : MonoBehaviour
{
    [SerializeField]
    private int _collisionNumber = 0;

    [HideInInspector]
    public int CollisionNumber { get { return  _collisionNumber;  } }


    /// <summary>
    /// Checks if A can collide with B according to collision number.
    /// </summary>
    public static bool CanCollide(CollisionMarker a, CollisionMarker b)
    {
        return a.CollisionNumber != b.CollisionNumber;
    }

    /// <summary>
    /// Checks is A can push B according to collision number.
    /// </summary>
    public static bool CanPush(CollisionMarker a, CollisionMarker b)
    {
        return a.CollisionNumber > b.CollisionNumber;
    }

    /// <summary>
    /// Checks is A can get pushed by B according to collision number.
    /// </summary>
    public static bool IsPushed(CollisionMarker a, CollisionMarker b)
    {
        return a.CollisionNumber < b.CollisionNumber;
    }
}
