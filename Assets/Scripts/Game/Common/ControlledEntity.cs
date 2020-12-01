using UnityEngine.Events;

/// <summary>
/// Static class that provides a reference to the most recently
/// controlled BoltEntity of this client; Useful for camera and UI
/// functions.
/// </summary>
[BoltGlobalBehaviour("GameScene")]
public class ControlledEntity : Bolt.GlobalEventListener
{
    public static BoltEntity controlledEntity = null;

    public class ControlledEntityEvent : UnityEvent<BoltEntity> { }
    public static ControlledEntityEvent onControlledEntitySwitch = new ControlledEntityEvent();

    public override void ControlOfEntityGained(BoltEntity entity)
    {
        controlledEntity = entity;
        onControlledEntitySwitch.Invoke(entity);
    }
}
