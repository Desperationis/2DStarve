using UnityEngine;

/// <summary>
/// Component that adds attacking functionality to an entity.
/// </summary>
public class AttackingComponent : MobBehaviour
{
    [SerializeField]
    [Tooltip("The hurtbox of this entity to ignore when attacking.")]
    protected BoxCollider2D hurtbox = null;

    [SerializeField]
    [Tooltip("The singular hitbox used for attacking in four directions.")]
    protected BoxCollider2D hitbox = null;

    [SerializeField]
    [Tooltip("The layer mask that determines what objects can be hit.")]
    protected LayerMask mask;


    /// <summary>
    /// Sends an attack event to all colliders (filtered by the layer mask) that
    /// are currently overlapping the attacking hitbox. This should be called
    /// as an animation event.
    /// </summary>
    public virtual void Attack()
    {
        if(entity.IsControllerOrOwner)
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.layerMask = mask;
            Collider2D[] hits = new Collider2D[5];

            hitbox.OverlapCollider(contactFilter, hits);

            for (int i = 0; i < hits.Length; i++)
            {
                Collider2D hit = hits[i];

                if (hit != null)
                {
                    if (hit != hurtbox)
                    {
                        // Send a event to the mob's health's TakeDamage() if possible.
                        hit.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }


    /// <summary>
    /// Rotates the hitbox around the transform according to the  cardinal
    /// direction the entity is facing; (0, -1) = 0 degrees and (-1, 0) = 270
    /// degrees.
    /// </summary>
    protected void FixedUpdate()
    {
        Vector2 cardinalDirection = mobController.cardinalDirection;
        Vector2 rotatedVector = new Vector2(-cardinalDirection.y, cardinalDirection.x);
        float directionalAngle = Mathf.Acos(rotatedVector.x) * (180.0f / Mathf.PI);
        directionalAngle *= cardinalDirection.x < 0 ? -1 : 1;

        hitbox.transform.eulerAngles = new Vector3(0, 0, directionalAngle);
    }

    public void Update()
    {
        if(entity.IsControlled)
        {
            if(mobAnimationController.IsPlaying("Attack"))
            {
                mobController.DisableMovement();
            }
            else
            {
                mobController.EnableMovement();
            }
        }
    }

    /// <summary>
    /// Triggers the trigger for the attack animation if it is not currently
    /// playing.
    /// </summary>
    public void TriggerAttackEvent()
    {
        if(!mobAnimationController.IsPlaying("Attack"))
        {
            mobAnimationController.ActivateTrigger("OnAttack");
        }
    }
}
