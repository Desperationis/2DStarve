using UnityEngine;

/// <summary>
/// Base class responsible for synching attack animations.
/// </summary>
public class AttackOrchestrator<T> : NetworkOrchestrator<T>
{
    [SerializeField]
    protected Animator animator = null;

    public override void OnEvent(EntityAttackEvent evnt)
    {
        // Call overrided function. 
        _OnEvent(evnt);
    }

    /// <summary>
    /// Overriden function that gets called in OnEvent(evnt) due to inherience issues;
    /// Activates the attack animation trigger.
    /// </summary>
    protected virtual void _OnEvent(EntityAttackEvent evnt)
    {
        animator.SetTrigger("OnAttack");
    }
}