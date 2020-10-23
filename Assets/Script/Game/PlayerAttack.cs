using UnityEngine;
using Bolt;
using Bolt.LagCompensation;

/// <summary>
/// Client-side prediction for player attack.
/// </summary>
public class PlayerAttack : EntityBehaviour<IPlayerState>
{
    [SerializeField]
    private float range = 5.0f;

    private bool attackedPressed = false;

    private void AttackNearby(Vector2 position)
    {
        BoltPhysicsHits hits = BoltNetwork.OverlapSphereAll(position, range);

        for (int i = 0; i < hits.count; i++)
        {
            BoltPhysicsHit hit = hits[i];

            if (hit.body.gameObject != gameObject)
            {
                hit.body.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
            }
        }
    }


    public override void ExecuteCommand(Command command, bool resetState)
    {
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        // Let the server know that the controller pressed attack
        if (cmd.Input.Attack && BoltNetwork.IsServer)
        {
            AttackNearby(cmd.Result.Position);
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
                AttackNearby(transform.position);
            }
            attackedPressed = Input.GetKey(KeyCode.Space);
        }
    }
}
