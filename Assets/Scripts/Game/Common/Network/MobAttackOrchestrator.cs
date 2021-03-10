/// <summary>
/// Makes attacking interfacible with AIBehaviors. Derives from 
/// AttackBase.
/// </summary>
public class MobAttackOrchestrator : AttackOrchestrator<IMobState>
{
    /// <summary>
    /// Updates state variables to match server.
    /// </summary>
    public override void SimulateOwner()
    {
        state.Attacking = attackingComponent.animationIsPlaying;
    }


    /// <summary>
    /// Match the attacking animation with the rest of the 
    /// clients.
    /// </summary>
    private void Update()
    {
        if (state.Attacking && !entity.IsOwner)
        {
            if (!attackingComponent.animationIsPlaying)
            {
                attackingComponent.TriggerAttackEvent();
            }
        }
    }
}
