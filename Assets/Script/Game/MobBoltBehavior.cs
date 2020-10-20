using UnityEngine;
using Bolt;

/// <summary>
/// Syncs all necessary variables for a mob from the server to the clients. 
/// </summary>
public class MobBoltBehavior : EntityBehaviour<IMobState>
{
    [SerializeField]
    [Tooltip("Used to syncronize movement settings.")]
    private MobController mobController;

    [SerializeField]
    [Tooltip("Syncs health with health bar.")]
    private MobHealthBar mobHealthBar;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);

        // Syncronize mobController settings on clients and server. 
        state.AddCallback("Direction", DirectionUpdate);
        state.AddCallback("Running", RunningUpdate);
        state.AddCallback("Health", HealthUpdate);

        if (BoltNetwork.IsServer)
        {
            state.Health = 100;
        }
    }

    private void HealthUpdate()
    {
        mobHealthBar.SetHealth(state.Health);
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
    }
}
