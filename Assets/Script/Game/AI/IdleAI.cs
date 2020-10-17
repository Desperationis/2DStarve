using UnityEngine;
using System;

/// <summary>
/// Allows a mob to idle.
/// </summary>
public class IdleAI : AIBehavior
{
    bool stayingStill = false;
    float switchTimer;

    public override void OnSwitchEnter()
    {
        switchTimer = Time.time + AIData.idleMoveTime;
    }

    public override void UpdateAI()
    {
        if(switchTimer < Time.time)
        {
            if(stayingStill)
            {
                mobController.SetDirection(UnityEngine.Random.insideUnitCircle);
            }
            else
            {
                mobController.SetDirection(Vector2.zero);
            }

            stayingStill = !stayingStill;
            switchTimer = Time.time + GetWaitingDelay();
        }

        mobController.UpdateFrame(Time.fixedDeltaTime);
    }

    public float GetWaitingDelay()
    {
        return stayingStill ? AIData.idleRestTime : AIData.idleMoveTime;
    }

    public override bool CheckRequirement()
    {
        // Always try to idle.
        return true;
    }

    public override Type GetIdentifier()
    {
        return typeof(IdleAI);
    }
}
