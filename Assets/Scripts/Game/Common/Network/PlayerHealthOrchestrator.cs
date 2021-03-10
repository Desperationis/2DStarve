/// <summary>
/// Implements the interface of IPlayerState into StateHealthBase logic. 
/// </summary>
public class PlayerHealthOrchestrator : HealthOrchestrator<IPlayerState>
{
    public override void LinkHealth()
    {
        state.AddCallback("Health", UpdateHealth);
    }

    protected override int GetStateHealth()
    {
        return state.Health;
    }

    protected override void SetStateHealth(int health)
    {
        state.Health = health;
    }
}
