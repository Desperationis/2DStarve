﻿using UnityEngine;
using Bolt;

/// <summary>
/// Syncs most variables for a mob from the server to the clients. Variables
/// that are extensively manipulated (e.x. health) have their own class.
/// </summary>
public class MobNetworkOrchestrator : NetworkOrchestrator<IMobState>
{
    [SerializeField]
    private AttackingComponent attackingComponent = null;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);

        // Syncronize mobController settings on clients and server. 
        state.AddCallback("Direction", DirectionUpdate);
        state.AddCallback("Running", RunningUpdate);
    }

    private void DirectionUpdate()
    {
        mobController.SetDirection(state.Direction);
    }

    private void RunningUpdate()
    {
        mobController.SetRunning(state.Running);
    }

    public override void SimulateOwner()
    {
        state.Direction = mobController.direction;
        state.Running = mobController.isRunning;

        if(attackingComponent != null)
        {
            state.Attacking = attackingComponent.animationIsPlaying;
        }
    }

    private void Update()
    {
        if(state.Attacking && !entity.IsOwner)
        {
            if (!attackingComponent.animationIsPlaying)
            {
                attackingComponent.TriggerAttackEvent();
            }
        }
    }

    public override void OnEvent(EntityVariableChangeEvent evnt)
    {
        mobController.SetSpeed(evnt.Speed);
        mobController.SetRunningMultiplier(evnt.RunningMultiplier);

        if(BoltNetwork.IsServer)
        {
            state.Health = evnt.Health;
        }
    }
}
