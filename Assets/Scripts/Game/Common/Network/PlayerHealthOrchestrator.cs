/// <summary>
/// Implements the interface of IPlayerState into StateHealthBase logic. 
/// </summary>
public class PlayerHealthOrchestrator : HealthOrchestrator<IPlayerState>
{
    public override void InitializeHealth()
    {
        state.AddCallback("Health", HealthUpdate);

        if (BoltNetwork.IsServer)
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
