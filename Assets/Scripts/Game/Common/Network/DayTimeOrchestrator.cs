using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

/// <summary>
/// Syncs daylight color based on server events
/// </summary>
public class DayTimeOrchestrator : Bolt.GlobalEventListener
{
    [SerializeField]
    private new Light2D light = null;

    public override void OnEvent(DayTimeChange evnt)
    {
        light.color = evnt.color;
    }
}
