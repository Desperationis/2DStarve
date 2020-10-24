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

    [SerializeField]
    [ReadOnly]
    private Vector2 _cardinalDirection = new Vector2(1, 0);

    public Vector2 CardinalDirection { get { return _cardinalDirection; } }


    /// <summary>
    /// Changes the state of the animation based on mobController's intended velocity and
    /// MobInformation.
    /// </summary>
    private void Update()
    {
        UpdateCardinalDirection(mobController.Direction);
        UpdateMovementVariables();
        FlipAnimation();
    }

    private void UpdateMovementVariables()
    {
        animator.SetFloat("horizontalDirection", _cardinalDirection.x);
        animator.SetFloat("verticalDirection", _cardinalDirection.y);
        animator.SetFloat("moving", mobController.Direction.sqrMagnitude != 0.0f ? 1 : -1);
        animator.SetFloat("running", mobController.Running ? 1 : -1);
    }

    private void UpdateCardinalDirection(Vector2 vector)
    {
        if (vector != Vector2.zero)
        {
            // Get the cardinal direction of the greater x or y component
            bool xComponentGreater = Mathf.Abs(vector.x) >= Mathf.Abs(vector.y);
            vector.x = xComponentGreater ? Mathf.Sign(vector.x) : 0;
            vector.y = !xComponentGreater ? Mathf.Sign(vector.y) : 0;

            _cardinalDirection = vector;
        }
    }

    public void FlipAnimation()
    {
        // X component determines the direction of flip. Mathf.Sign(0) = 1
        float inverseDirection = flipDirection ? -1 : 1;
        float direction = Mathf.Sign(_cardinalDirection.x * inverseDirection);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }
}
