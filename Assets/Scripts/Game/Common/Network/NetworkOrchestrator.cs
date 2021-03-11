using UnityEngine;

/// <summary>
/// Base class for anything that needs to be synched over
/// Photon Bolt.
/// </summary>
/// <typeparam name="T">Interface for the entity.</typeparam>
public class NetworkOrchestrator<T> : Bolt.EntityEventListener<T>
{
    protected MobController mobController = null;
    protected MobAnimationController mobAnimationController = null;

    protected virtual void Awake()
    {
        mobController = GetComponent<MobController>();

        if (mobController == null)
        {
            mobController = GetComponentInParent<MobController>();
        }

        mobAnimationController = GetComponent<MobAnimationController>();

        if (mobAnimationController == null)
        {
            mobAnimationController = GetComponentInParent<MobAnimationController>();
        }

        _Awake();
    }

    protected virtual void _Awake()
    {

    }
}
