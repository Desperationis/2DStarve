using UnityEngine;


/// <summary>
/// Implements the interface of IPlayerState into StateHealthBase logic. 
/// </summary>
public class PlayerStateHealth : StateHealthBase<IPlayerState>
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
