using UnityEngine;
using Bolt;

/// <summary>
/// Client-side prediction for player attack. Derives from  AttackBase.
/// </summary>
public class PlayerAttackOrchestrator : AttackOrchestrator<IPlayerState>
{
    public override void ExecuteCommand(Command command, bool resetState)
    {
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        if(!resetState && cmd.Input.Attack && !mobAnimationController.IsPlaying("Attack"))
        {
            // Attack on input
            if (BoltNetwork.IsServer)
            {
                attackingComponent.TriggerAttackEvent();
                EntityAttackEvent attackEvent = EntityAttackEvent.Create(entity);
                attackEvent.Send();
            }
            if (BoltNetwork.IsClient)
            {
                attackingComponent.TriggerAttackEvent();
            }
        }
    }

    public override void OnEvent(EntityAttackEvent evnt)
    {
        if(!entity.IsControllerOrOwner)
        {
            attackingComponent.TriggerAttackEvent();
        }
    }
}
