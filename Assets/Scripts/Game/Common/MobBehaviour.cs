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

    protected virtual void Awake()
    {
        mobController = GetComponent<MobController>();

        if(mobController == null)
        {
            mobController = GetComponentInParent<MobController>();
        }
        if (mobController == null)
        {
            mobController = GetComponentInChildren<MobController>();
        }

        mobAnimationController = GetComponent<MobAnimationController>();

        if (mobAnimationController == null)
        {
            mobAnimationController = GetComponentInChildren<MobAnimationController>();
        }
        if (mobAnimationController == null)
        {
            mobAnimationController = GetComponentInParent<MobAnimationController>();
        }
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
    }
}
