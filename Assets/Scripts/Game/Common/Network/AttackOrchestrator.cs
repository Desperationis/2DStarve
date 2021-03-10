using UnityEngine;

/// <summary>
/// Base class responsible for synching attack animations.
/// </summary>
public class AttackOrchestrator<T> : NetworkOrchestrator<T>
{
    [SerializeField]
    protected AttackingComponent attackingComponent = null;

    public override void OnEvent(EntityAttackEvent evnt)
    {
        // Call overrided function. 
        _OnEvent(evnt);
    }

    /// <summary>
    /// Overriden function that gets called in OnEvent(evnt) due to inheritence issues;
    /// Activates the attack animation trigger.
    /// </summary>
    protected virtual void _OnEvent(EntityAttackEvent evnt)
    {
        attackingComponent.TriggerAttackEvent();
    }
}