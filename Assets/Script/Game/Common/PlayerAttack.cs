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
    private float range = 5.0f;

    private bool attackedPressed = false;

    public void AttackNearby()
    {
        if(entity.IsControllerOrOwner)
        {
            AttackNearby(transform.position);
        }
    }

    private void AttackNearby(Vector2 position)
    {
        BoltPhysicsHits hits = BoltNetwork.OverlapSphereAll(position, range);

        for (int i = 0; i < hits.count; i++)
        {
            BoltPhysicsHit hit = hits[i];

            if (hit.body.gameObject != transform.parent.gameObject)
            {
                hit.body.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
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
}
