using UnityEngine;

/// <summary>
/// Makes attacking interfacible with AIBehaviors. Derives from 
/// AttackBase.
/// </summary>
public class MobAttack : AttackBase<IMobState>
{
    /// <summary>
    /// Only attack (do damage) on the server. Actual values will
    /// be synced with EntityState with MobStateHealth.
    /// </summary>
    public override void Attack()
    {
        if (entity.IsOwner)
        {
            base.Attack();
        }
    }

    /// <summary>
    /// Triggers the attack animation event on all clients that 
    /// are listening to this entity.
    /// </summary>
    public void TriggerAttackEvent()
    {
        animator.SetTrigger("OnAttack");
    }
}
