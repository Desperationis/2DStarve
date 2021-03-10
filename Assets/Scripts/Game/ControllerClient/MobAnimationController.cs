using UnityEngine;

/// <summary>
/// Controls the animation variables of a mob's animator.
/// </summary>
public class MobAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;

    [SerializeField]
    private MobController mobController = null;

    [SerializeField]
    private bool flipDirection = false;

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
    public void FlipAnimation()
    {
        // X component determines the direction of flip. Mathf.Sign(0) = 1
        float inverseDirection = flipDirection ? -1 : 1;
        float direction = Mathf.Sign(mobController.cardinalDirection.x * inverseDirection);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }
}
