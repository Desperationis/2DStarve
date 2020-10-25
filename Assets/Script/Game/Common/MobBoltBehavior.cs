using UnityEngine;
using Bolt;

/// <summary>
/// Syncs most variables for a mob from the server to the clients. Variables
/// that are extensively manipulated (e.x. health) have their own class.
/// </summary>
public class MobBoltBehavior : EntityBehaviour<IMobState>
{
    [SerializeField]
    [Tooltip("Used to syncronize movement settings.")]
    private MobController mobController = null;

    [SerializeField]
    [Tooltip("Used to sync attack animations.")]
    private MobAttack mobAttack = null;

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
        state.Direction = mobController.Direction;
        state.Running = mobController.Running;

        if(mobAttack != null)
        {
            state.Attacking = mobAttack.animationIsPlaying;
        }
    }

    private void Update()
    {
        if(state.Attacking && !entity.IsOwner)
        {
            if (!mobAttack.animationIsPlaying)
            {
                mobAttack.TriggerAttackEvent();
            }
        }
    }
}
