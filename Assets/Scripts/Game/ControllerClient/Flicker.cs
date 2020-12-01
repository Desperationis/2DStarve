using UnityEngine;

/// <summary>
/// Flickers the local scale of a sprite.
/// </summary>
public class Flicker : MonoBehaviour
{
    [SerializeField]
    private float min = 3.0f;

    [SerializeField]
    private float max = 5.0f;

    [SerializeField]
    private float speed = 0.1f;

    private void FixedUpdate()
    {
        float targetRadius = Random.Range(min, max);
        float newScale = Mathf.MoveTowards(transform.localScale.x, targetRadius, speed);

        transform.localScale = new Vector3(newScale, newScale, 1);
    }
}
