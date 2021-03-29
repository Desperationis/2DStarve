using UnityEngine;
using Bolt;

/// <summary>
/// Client-side prediction for player attack. Derives from  AttackBase.
/// </summary>
public class PlayerAttackOrchestrator : NetworkOrchestrator<IPlayerState>
{
    [SerializeField]
    private AttackingComponent attackingComponent = null;

    public override void ExecuteCommand(Command command, bool resetState)
    {
        PlayerMovementAuth cmd = (PlayerMovementAuth)command;

        if(!resetState && cmd.Input.Attack && !attackingComponent.isAttacking)
        {
            // Attack on input
            if (BoltNetwork.IsServer)
            {
                if(attackingComponent.Attack())
                {
                    EntityAttackEvent attackEvent = EntityAttackEvent.Create(entity);
                    attackEvent.Send();
                }
            }
            if (BoltNetwork.IsClient)
            {
                attackingComponent.Attack();
            }
        }
    }

    public override void OnEvent(EntityAttackEvent evnt)
    {
        if(!entity.IsControllerOrOwner)
        {
            attackingComponent.Attack();
        }
    }
}
