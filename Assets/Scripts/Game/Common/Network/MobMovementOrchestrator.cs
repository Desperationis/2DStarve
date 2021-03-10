using UnityEngine;
using Bolt;

/// <summary>
/// Syncs most variables for a mob from the server to the clients. Variables
/// that are extensively manipulated (e.x. health) have their own class.
/// </summary>
public class MobMovementOrchestrator : MovementOrchestrator<IMobState>
{
    [SerializeField]
    private AttackingComponent attackingComponent = null;


    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
        state.AddCallback("Direction", DirectionUpdate);
        state.AddCallback("Running", RunningUpdate);
    }


    protected override void DirectionUpdate()
    {
        mobController.SetDirection(state.Direction);
    }


    protected override void RunningUpdate()
    {
        mobController.SetRunning(state.Running);
    }


    /// <summary>
    /// Updates state variables to match server.
    /// </summary>
    public override void SimulateOwner()
    {
        state.Direction = mobController.direction;
        state.Running = mobController.isRunning;
    }
}
