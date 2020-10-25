using UnityEngine;

/// <summary>
/// Base class for attacking other entities. 
/// </summary>
public class AttackBase<T> : Bolt.EntityEventListener<T>
{
    [SerializeField]
    [Tooltip("Used to activate the attack animation trigger.")]
    protected Animator animator = null;

    [SerializeField]
    [Tooltip("The hurtbox of this entity to ignore when attacking.")]
    protected BoxCollider2D hurtbox = null;

    [SerializeField]
    [Tooltip("The singular hitbox used for attacking in four directions.")]
    protected BoxCollider2D hitbox = null;

    [SerializeField]
    [Tooltip("Used to determine the location of the hitbox.")]
    protected MobAnimationController mobAnimationController = null;

    [SerializeField]
    [Tooltip("The layer mask that determines what objects can be hit.")]
    protected LayerMask mask;

    public bool animationIsPlaying
    {
        get {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack");
        }
        
    }


    /// <summary>
    /// Sends an attack event to all colliders (filtered by a layer mask) that
    /// are currently overlapping the hitbox. This should be put into an 
    /// animation event. 
    /// </summary>
    public virtual void Attack()
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
                    hit.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }


    public override void OnEvent(EntityAttackEvent evnt)
    {
        _OnEvent(evnt);
    }

    /// <summary>
    /// Buffer function that passes directly into EntityEventListener.OnEvent(evnt);
    /// Activates the attack animation trigger.
    /// </summary>
    protected virtual void _OnEvent(EntityAttackEvent evnt)
    {
        animator.SetTrigger("OnAttack");
    }


    /// <summary>
    /// Rotates the hitbox around the transform according to the 
    /// cardinal direction the entity is facing; (0, -1) = 0 degrees and
    /// (-1, 0) = 270 degrees.
    /// </summary>
    protected void FixedUpdate()
    {
        Vector2 cardinalDirection = mobAnimationController.CardinalDirection;
        Vector2 rotatedVector = new Vector2(-cardinalDirection.y, cardinalDirection.x);
        float directionalAngle = Mathf.Acos(rotatedVector.x) * (180.0f / Mathf.PI);
        directionalAngle *= cardinalDirection.x < 0 ? -1 : 1;

        hitbox.transform.eulerAngles = new Vector3(0, 0, directionalAngle);
    }


}
