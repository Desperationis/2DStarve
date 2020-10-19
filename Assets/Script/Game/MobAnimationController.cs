﻿using UnityEngine;

public class MobAnimationController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private MobController mobController;

    [SerializeField]
    private bool flipDirection = false;

    [SerializeField]
    [ReadOnly]
    private Vector2 cardinalDirection = new Vector2(1, 0);


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
        animator.SetInteger("horizontalDirection", (int)cardinalDirection.x);
        animator.SetInteger("verticalDirection", (int)cardinalDirection.y);
        animator.SetBool("moving", mobController.Direction.sqrMagnitude != 0.0f);
        animator.SetBool("running", mobController.Running);
    }

    private void UpdateCardinalDirection(Vector2 vector)
    {
        if (vector != Vector2.zero)
        {
            // Get the cardinal direction of the greater x or y component
            if (Mathf.Abs(vector.x) >= Mathf.Abs(vector.y))
            {
                vector.y = 0.0f;
                vector.x = Mathf.Sign(vector.x);
            }
            else
            {
                vector.x = 0.0f;
                vector.y = Mathf.Sign(vector.y);
            }

            cardinalDirection = vector;
        }
    }

    public void FlipAnimation()
    {
        // X component determines the direction of flip. Mathf.Sign(0) = 1
        float inverseDirection = flipDirection ? -1 : 1;
        float direction = Mathf.Sign(cardinalDirection.x * inverseDirection);

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }
}