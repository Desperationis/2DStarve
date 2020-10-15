using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to mark the collision number of an entity; In relation
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


    public bool CanCollide(int collisionNumber)
    {
        return collisionNumber != CollisionNumber;
    }

    public bool CanPush(int collisionNumber)
    {
        return CollisionNumber > collisionNumber;
    }

    public bool IsPushed(int collisionNumber)
    {
        return CollisionNumber < collisionNumber;
    }
}
