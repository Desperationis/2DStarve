using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class WolfTestScript : Bolt.EntityBehaviour<IMobState>
{
    public float range = 2.0f;
    public float speed = 3.0f;
    public BoxCollider2D boxCollider;
    public CollisionMarker collisionMarker;

    private void CalculateCollidedPosition()
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
                    transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
                }
            }
        }
    }


    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }

    public void Update()
    {
        foreach(var player in PlayerRegistry.AllPlayers)
        {
            Vector3 position = player.character.gameObject.transform.position;
            Vector3 difference = position - transform.position;

            if(difference.sqrMagnitude < Mathf.Pow(range, 2))
            {
                transform.position += difference.normalized * speed * BoltNetwork.FrameDeltaTime;
                break; 
            }
        }
        CalculateCollidedPosition();
    }
}
