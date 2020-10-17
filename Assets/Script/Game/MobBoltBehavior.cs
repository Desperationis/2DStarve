using UnityEngine;
using Bolt;

public class MobBoltBehavior : EntityBehaviour<IMobState>
{
    public override void Attached()
    {
        state.SetTransforms(state.Transform, transform);
    }
}
