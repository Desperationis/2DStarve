using UnityEngine;

public class CloseAttackAI : AIBehavior
{
    [SerializeField]
    private MobAttack mobAttack;

    public override void UpdateAI()
    {
        mobController.SetDirection(Vector2.zero);
        mobController.UpdateFrame(Time.fixedDeltaTime);

        if(!mobAttack.animationIsPlaying)
        {
            mobAttack.TriggerAttackEvent();
        }
    }


    public override bool CheckRequirement()
    {
        return PlayerRegistry.OverlapCircleAny(transform.position, 1.5f) || mobAttack.animationIsPlaying;
    }
}
