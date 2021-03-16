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
    protected DataComponent dataComponent = null;

    /// <summary>
    /// Searches for a component in a mob in its children and parent.
    /// Priority is current->children->parent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public U SearchComponentInMob<U>()
    {
        U component = GetComponent<U>();

        if (component == null)
        {
            component = GetComponentInChildren<U>();
        }
        if (component == null)
        {
            component = GetComponentInParent<U>();
        }

        return component;
    }

    protected virtual void Awake()
    {
        mobController = SearchComponentInMob<MobController>();
        mobAnimationController = SearchComponentInMob<MobAnimationController>();
        dataComponent = SearchComponentInMob<DataComponent>();
    }
}
