using UnityEngine;

/// <summary>
/// Syncs daylight color based on server events
/// </summary>
public class DayTimeOrchestrator : Bolt.GlobalEventListener
{
    [SerializeField]
    private new SpriteRenderer light = null;

    public override void OnEvent(DayTimeChange evnt)
    {
        light.color = evnt.color;
    }
}
