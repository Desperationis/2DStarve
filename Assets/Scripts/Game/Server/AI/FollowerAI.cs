using UnityEngine;

/// <summary>
/// Allows a mob to follow the nearest player.
/// </summary>
[RequireComponent(typeof(MobController))]
public class FollowerAI : AIBehavior
{
    public override void UpdateAI()
    {
        PlayerObject nearestPlayer = PlayerRegistry.OverlapCircle(transform.position, dataComponent.aiData.followerRange);

        if(nearestPlayer != null)
        {
            mobController.SetRunning(true);
            mobController.SetDirection(nearestPlayer.character.transform.position - transform.position);
            mobController.UpdateFrame();
        }

    }

    public override bool CheckRequirement()
    {
        return PlayerRegistry.OverlapCircleAny(transform.position, dataComponent.aiData.followerRange);
    }

    public override void OnSwitchLeave()
    {
        mobController.SetRunning(false);
        mobController.SetDirection(Vector2.zero);
    }
}
