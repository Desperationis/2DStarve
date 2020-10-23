using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Implements the interface of IMobState into StateHealthBase logic. 
/// </summary>
public class MobStateHealth : StateHealthBase<IMobState>
{
    public override void InitializeHealth()
    {
        state.AddCallback("Health", HealthUpdate);

        if(BoltNetwork.IsServer)
        {
            state.Health = 100;
        }
    }

    protected override int _GetStateHealth()
    {
        return state.Health;
    }

    protected override void _SetStateHealth(int health)
    {
        state.Health = health;
    }
}
