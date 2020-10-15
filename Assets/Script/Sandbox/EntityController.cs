using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EntityController : MonoBehaviour
{
    [SerializeField, Tooltip("Max speed, in units per second, that the character moves.")]
    float speed = 9;

    [SerializeField, Tooltip("Acceleration while grounded.")]
    float walkAcceleration = 75;

    private BoxCollider2D boxCollider;

    private Vector2 velocity;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);

        moveInput = Input.GetAxisRaw("Vertical");
        velocity.y = Mathf.MoveTowards(velocity.y, speed * moveInput, walkAcceleration * Time.deltaTime);

        transform.Translate(velocity * Time.deltaTime);

        // Collision

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider.size, 0);

        foreach(Collider2D hit in hits)
        {
            if(hit == boxCollider)
            {
                continue;
            }

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

            if(colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
            }
        }
    }
}
