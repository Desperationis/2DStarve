using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Implements the interface of IMobState into StateHealthBase logic. 
/// </summary>
public class MobStateHealth : StateHealthBase<IMobState>
{
    public override void Attached()
    {
        state.AddCallback("Health", HealthUpdate);
        base.Attached();
    }

    public override int GetStateHealth()
    {
        return state.Health;
    }

    protected override void _SetStateHealth(int health)
    {
        state.Health = health;
    }
}
