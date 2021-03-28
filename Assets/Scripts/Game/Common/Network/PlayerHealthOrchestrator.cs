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
        // Check if this entity is not being destroyed before accessing bolt
        // properties.
        if (entity.IsAttached)
        {
            return state.Health;
        }

        return 0;
    }

    protected override void SetStateHealth(int health)
    {
        if (entity.IsAttached)
        {
            state.Health = health;
        }
    }
}
