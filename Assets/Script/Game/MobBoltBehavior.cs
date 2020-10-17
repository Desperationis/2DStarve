using UnityEngine;
using Bolt;

public class MobBoltBehavior : EntityBehaviour<IMobState>
{
    [SerializeField]
    private MobController mobController;

    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
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
    }
}
