using UnityEngine;
using System;

/// <summary>
/// Allows a mob to follow the nearest player.
/// </summary>
[RequireComponent(typeof(MobController))]
public class FollowerAI : AIBehavior
{
    public override void UpdateAI()
    {
        BoltEntity nearestPlayer = GetNearestPlayer();
        mobController.SetRunning(true);
        mobController.SetDirection(nearestPlayer.transform.position - transform.position);
        mobController.UpdateFrame(Time.fixedDeltaTime);
    }

    public override bool CheckRequirement()
    {
        return PlayerNearby();
    }

    public override void OnSwitchLeave()
    {
        mobController.SetRunning(false);
        mobController.SetDirection(Vector2.zero);
    }

    private bool PlayerNearby()
    {
        foreach (PlayerObject player in PlayerRegistry.AllPlayers)
        {
            Vector2 position = player.character.transform.position;
            Vector2 difference = (Vector2)transform.position - position;

            if (difference.sqrMagnitude < Mathf.Pow(AIData.followerRange, 2))
            {
                return true;
            }
        }

        return false;
    }

    private BoltEntity GetNearestPlayer()
    {
        BoltEntity closestPlayer = null;
        Vector2 nearestDistance = Vector2.positiveInfinity;

        foreach(PlayerObject player in PlayerRegistry.AllPlayers)
        {
            Vector2 position = player.character.transform.position;
            Vector2 difference = (Vector2)transform.position - position;

            if(difference.sqrMagnitude < Mathf.Pow(AIData.followerRange, 2))
            {
                if(difference.sqrMagnitude < nearestDistance.sqrMagnitude)
                {
                    closestPlayer = player.character;
                    nearestDistance = difference;
                }
            }
        }

        return closestPlayer;
    }
}
