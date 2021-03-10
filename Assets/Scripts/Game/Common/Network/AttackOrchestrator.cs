using UnityEngine;

/// <summary>
/// Base class responsible for synching attack animations.
/// </summary>
public class AttackOrchestrator<T> : NetworkOrchestrator<T>
{
    [SerializeField]
    protected AttackingComponent attackingComponent = null;
}