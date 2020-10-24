using UnityEngine;
using Bolt;
using Bolt.LagCompensation;

/// <summary>
/// Client-side prediction for player attack.
/// </summary>
public class PlayerAttack : EntityEventListener<IPlayerState>
{
    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private BoxCollider2D hitbox = null;

    [SerializeField]
    private MobAnimationController mobAnimationController = null;

    private bool attackedPressed = false;

    public LayerMask mask;

    public void AttackNearby()
    {
        if(entity.IsControllerOrOwner)
        {
            AttackNearby(transform.position);
        }
    }

    private void AttackNearby(Vector2 position)
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = mask;
        Collider2D[] hits = new Collider2D[5];

        hitbox.OverlapCollider(contactFilter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            Collider2D hit = hits[i];

            if(hit != null)
            {
                if (hit.gameObject != transform.parent.gameObject)
                {
                    hit.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }


    public override void ExecuteCommand(Command command, bool resetState)
    {
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        // Let the server know that the controller pressed attack
        // Since it's only server-side, will only be executed once.
        if (cmd.Input.Attack && BoltNetwork.IsServer)
        {
            animator.SetTrigger("OnAttack");
            EntityAttackEvent attackEvent = EntityAttackEvent.Create(entity);
            attackEvent.Send();
        }
    }

    public override void OnEvent(EntityAttackEvent evnt)
    {
        if(!entity.IsControllerOrOwner)
        {
            animator.SetTrigger("OnAttack");
        }
    }

    public override void SimulateController()
    {
        // Client-side prediction of attack; We don't want to travel
        // back in time with Photon Bolt commands
        if(BoltNetwork.IsClient)
        {
            if(Input.GetKey(KeyCode.Space) && !attackedPressed)
            {
                animator.SetTrigger("OnAttack");
            }
            attackedPressed = Input.GetKey(KeyCode.Space);
        }
    }

    public void Update()
    {
        Vector2 cardinalDirection = mobAnimationController.CardinalDirection;
        Vector2 rotatedVector = new Vector2(-cardinalDirection.y, cardinalDirection.x);
        float directionalAngle = Mathf.Acos(rotatedVector.x) * (180.0f / Mathf.PI);
        directionalAngle *= cardinalDirection.x < 0 ? -1 : 1;

        hitbox.transform.eulerAngles = new Vector3(0, 0, directionalAngle);

    }
}
