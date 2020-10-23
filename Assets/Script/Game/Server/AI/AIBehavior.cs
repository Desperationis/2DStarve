﻿using UnityEngine;

/// <summary>
/// Base Class for all AI behaviors.
/// </summary>
[RequireComponent(typeof(AISwapper))]
public class AIBehavior : MonoBehaviour
{
    protected MobController mobController = null;
    protected MobAIData AIData = null;

    public virtual void SetDependencies(MobController mobController, MobAIData AIData)
    {
        this.mobController = mobController;
        this.AIData = AIData;
    }


    /// <summary>
    /// Called when AISwapper is initialized.
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
    /// Updates AIBehavior on FixedUpdate(). This will only
    /// be called if the AI is currently swapped in. 
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
}