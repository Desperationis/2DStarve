using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// Flickers  a Light2D
/// </summary>
public class Flicker : MonoBehaviour
{
    [SerializeField]
    private new Light2D light = null;

    [SerializeField]
    private float min = 3.0f;

    [SerializeField]
    private float max = 5.0f;

    [SerializeField]
    private float speed = 0.1f;

    private void FixedUpdate()
    {
        float targetRadius = Random.Range(min, max);
        float newRadius = Mathf.MoveTowards(light.pointLightOuterRadius, targetRadius, speed);

        light.pointLightOuterRadius = newRadius;
    }
}
