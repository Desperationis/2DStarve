using UnityEngine;
using System;

/// <summary>
/// Abstract Class for all AI behaviors.
/// </summary>
[RequireComponent(typeof(AISwapper))]
public class AIBehavior : MonoBehaviour
{
    protected MobController mobController;
    protected MobAIData AIData;

    public virtual void SetDependencies(MobController mobController, MobAIData AIData)
    {
        this.mobController = mobController;
        this.AIData = AIData;
    }


    /// <summary>
    /// Called when AISwapper starts up.
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// Called when AISwapper swaps into this AI.
    /// </summary>
    public virtual void OnSwitchEnter()
    {

    }

    /// <summary>
    /// A rule that needs to be fulfilled for this AI to be swapped in.
    /// If true, AISwapper will try to swap this AI for another, so long as its
    /// priority is high enough.
    /// </summary>
    /// 
    /// <returns>
    /// Returns a boolean, representing if the rule was fulfilled or not.
    /// </returns>
    public virtual bool CheckRequirement()
    {
        return false;
    }

    /// <summary>
    /// Updates AIBehavior.
    /// </summary>
    public virtual void UpdateAI()
    {

    }

    /// <summary>
    /// Called when AISwapper swaps this AI for another.
    /// </summary>
    public virtual void OnSwitchLeave()
    {

    }

    /// <summary>
    /// Used to identity the current AI by its type.
    /// </summary>
    public virtual Type GetIdentifier()
    {
        return this.GetType();
    }
}
