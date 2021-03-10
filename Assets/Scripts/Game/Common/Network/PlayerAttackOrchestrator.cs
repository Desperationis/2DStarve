using UnityEngine;
using Bolt;

/// <summary>
/// Client-side prediction for player attack. Derives from 
/// AttackBase.
/// </summary>
public class PlayerAttackOrchestrator : AttackOrchestrator<IPlayerState>
{
    [SerializeField]
    private AttackingComponent attackingComponent = null;

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

    protected override void _OnEvent(EntityAttackEvent evnt)
    {
        if(!entity.IsControllerOrOwner)
        {
            base._OnEvent(evnt);
        }
    }

    public override void SimulateController()
    {
        // Client-side prediction of attack; We don't want to travel
        // back in time with Photon Bolt commands
        if(BoltNetwork.IsClient)
        {
            if(Input.GetKey(KeyCode.Space) && !attackingComponent.animationIsPlaying)
            {
                animator.SetTrigger("OnAttack");
            }
        }
    }
}
