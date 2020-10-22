using UnityEngine;
using Bolt;
using Bolt.LagCompensation;

/// <summary>
/// Client-side prediction for player attack.
/// </summary>
public class PlayerAttack : EntityBehaviour<IPlayerState>
{
    [SerializeField]
    private PlayerStateHealth playerHealth;

    [SerializeField]
    private float range = 5.0f;

    public override void ExecuteCommand(Command command, bool resetState)
    {
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        if(resetState)
        {

        }
        else
        {
            if (cmd.Input.Attack)
            {
                BoltPhysicsHits hits = BoltNetwork.OverlapSphereAll(cmd.Result.Position, range, cmd.ServerFrame);

                for (int i = 0; i < hits.count; i++)
                {
                    BoltPhysicsHit hit = hits[i];

                    if(hit.body.gameObject != gameObject)
                    {
                        hit.body.gameObject.SendMessage("TakeDamage", 10, SendMessageOptions.DontRequireReceiver);
                    }
                }
            }
        }
    }

    /// <summary>
    /// A callback function to a gameObject message.
    /// </summary>
    public void TakeDamage(int amount)
    {
        playerHealth.SetStateHealth(playerHealth.GetStateHealth() - amount);
    }
}
