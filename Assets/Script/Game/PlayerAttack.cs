﻿using UnityEngine;
using Bolt;

/// <summary>
/// Client-side prediction for player attack.
/// </summary>
public class PlayerAttack : AttackBase<IPlayerState>
{
    private bool attackedPressed = false;

    public override void Attack()
    {
        if (entity.IsControllerOrOwner)
        {
            base.Attack();
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
            if(Input.GetKey(KeyCode.Space) && !attackedPressed)
            {
                animator.SetTrigger("OnAttack");
            }
            attackedPressed = Input.GetKey(KeyCode.Space);
        }
    }
}