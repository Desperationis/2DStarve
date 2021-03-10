/// <summary>
/// Implements the interface of IMobState into StateHealthBase logic. 
/// </summary>
public class MobHealthOrchestrator : HealthOrchestrator<IMobState>
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
