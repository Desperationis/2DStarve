using UnityEngine;
using Bolt;

/// <summary>
/// Bolt.EntityBehaviour-derived class that exposes
/// an easy reference to a Mob's MobController.
/// </summary>
public class MobBehaviour : Bolt.EntityBehaviour
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
    public T SearchComponentInMob<T>() {
        T component = GetComponent<T>();
        
        if (component == null)
        {
            component = GetComponentInChildren<T>();
        }
        if (component == null)
        {
            component = GetComponentInParent<T>();
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

/// <summary>
/// Bolt.EntityBehaviour-derived class that exposes
/// an easy reference to a Mob's MobController.
/// </summary>
public class MobBehaviour<T> : Bolt.EntityBehaviour<T>
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
    public T SearchComponentInMob<T>()
    {
        T component = GetComponent<T>();

        if (component == null)
        {
            component = GetComponentInChildren<T>();
        }
        if (component == null)
        {
            component = GetComponentInParent<T>();
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
