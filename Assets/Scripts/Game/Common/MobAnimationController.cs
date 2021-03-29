using UnityEngine;

/// <summary>
/// Controls the animation variables of a mob's animator and gives information
/// about them.
/// </summary>
public class MobAnimationController : MobBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer = null;

    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private bool inverseDirection = false;


    /// <summary>
    /// Whether or not a specific blend tree is playing; I.e. attack.
    /// </summary>
    /// <param name="name">Name of the blend tree</param>
    public bool IsPlaying(string name)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    }

    /// <summary>
    /// Activates an animation trigger.
    /// </summary>
    /// <param name="name">Name of trigger</param>
    public void ActivateTrigger(string name)
    {
        animator.SetTrigger(name);
    }


    private void Update()
    {
        UpdateMovementVariables();
        FlipAnimation();
    }

    private void UpdateMovementVariables()
    {
        animator.SetFloat("horizontalDirection", mobController.cardinalDirection.x);
        animator.SetFloat("verticalDirection", mobController.cardinalDirection.y);
        animator.SetFloat("moving", mobController.isMoving ? 1 : -1);
        animator.SetFloat("running", mobController.isRunning && mobController.isMoving ? 1 : -1);
    }
    private void FlipAnimation()
    {
        // X component determines the direction of flip.
        bool flipRightNow = mobController.cardinalDirection.x < 0 ? true : false;

        flipRightNow = inverseDirection ? !flipRightNow : flipRightNow;

        spriteRenderer.flipX = flipRightNow;
    }
}
