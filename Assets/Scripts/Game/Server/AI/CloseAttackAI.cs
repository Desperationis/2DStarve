using UnityEngine;

public class CloseAttackAI : AIBehavior
{
    [SerializeField]
    [Tooltip("Used to trigger the attack animation over and over.")]
    private AttackingComponent attackingComponent = null;

    float attackTimer;

    public override void OnSwitchEnter()
    {
        mobController.DisableMovement();
        attackTimer = Time.time;
    }

    public override void UpdateAI()
    {
        PlayerObject closestPlayer = PlayerRegistry.OverlapCircle(transform.position, 1.5f);

        if(closestPlayer != null)
        {
            mobController.SetDirection(closestPlayer.character.transform.position - transform.position);
        }
        mobController.UpdateFrame();

        if(!attackingComponent.isAttacking && attackTimer < Time.time)
        {
            attackingComponent.Attack();
            attackTimer = Time.time + dataComponent.aiData.attackPause;
        }
    }


    public override bool CheckRequirement()
    {
        return PlayerRegistry.OverlapCircleAny(transform.position, 1.5f) || attackingComponent.isAttacking || attackTimer > Time.time;
    }

    public override void OnSwitchLeave()
    {
        mobController.EnableMovement();
    }
}
