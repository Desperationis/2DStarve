using UnityEngine;

/// <summary>
/// Syncs the attacking component on mobs with clients.
/// </summary>
public class MobAttackOrchestrator : NetworkOrchestrator<IMobState>
{
    [SerializeField]
    private AttackingComponent attackingComponent = null;

    public override void SimulateOwner()
    {
        state.Attacking = mobAnimationController.IsPlaying("Attack");
    }


    /// <summary>
    /// Match the attacking animation with the rest of the  clients.
    /// </summary>
    private void Update()
    {
        if (state.Attacking && !entity.IsOwner)
        {
            attackingComponent.Attack();
        }
    }
}
