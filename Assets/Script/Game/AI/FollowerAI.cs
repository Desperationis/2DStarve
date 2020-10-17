using UnityEngine;
using System;

/// <summary>
/// Allows a mob to follow the nearest player.
/// </summary>
[RequireComponent(typeof(MobController))]
public class FollowerAI : AIBehavior
{
    public override void OnSwitchEnter()
    {
        mobController.SetRunning(AIData.runAtFollow);
    }
    /*
    public override void UpdateAI()
    {
        PlayerRegistry.


        // Move toward closest player.
        GameObject closestPlayer = PlayerManager.Instance.GetNearestPlayer(transform, information.mobData.followerRange);

        if(closestPlayer != null)
        {
            MoveTowardPlayer(closestPlayer.transform);
        }
    }

    public override bool CheckRequirement()
    {
        return PlayerManager.Instance.GetNearestPlayer(transform, information.mobData.followerRange) != null;
    }

    public override void OnSwitchLeave()
    {

    }

    public override Type GetIdentifier()
    {
        return typeof(FollowerAI);
    }

    public void MoveTowardPlayer(Transform playerTransform)
    {
        // Get and set unit velocity relative to player.
        Vector2 direction = (Vector2) playerTransform.position - (Vector2)transform.position;
        movement.intendedDirection = direction;
    }*/
}
