using UnityEngine;

public class CloseAttackAI : AIBehavior
{
    [SerializeField]
    [Tooltip("Used to trigger the attack animation over and over.")]
    private AttackingComponent attackingComponent = null;

    public override void OnSwitchEnter()
    {
        mobController.DisableMovement();
    }

    public override void UpdateAI()
    {
        PlayerObject closestPlayer = PlayerRegistry.OverlapCircle(transform.position, 1.5f);

        if(closestPlayer != null)
        {
            mobController.SetDirection(closestPlayer.character.transform.position - transform.position);
        }
        mobController.UpdateFrame(Time.fixedDeltaTime);

        if(!mobAnimationController.IsPlaying("Attack"))
        {
            attackingComponent.TriggerAttackEvent();
        }
    }


    public override bool CheckRequirement()
    {
        return PlayerRegistry.OverlapCircleAny(transform.position, 1.5f) || mobAnimationController.IsPlaying("Attack");
    }

    public override void OnSwitchLeave()
    {
        mobController.EnableMovement();
    }
}
