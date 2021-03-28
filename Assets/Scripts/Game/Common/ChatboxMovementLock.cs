using UnityEngine;

/// <summary>
/// Locks the currently controlled player in position while typing by completely
/// disabling the player from sending input commands.
/// </summary>
public class ChatboxMovementLock : MonoBehaviour
{
    /// Functions below should ONLY go below OnSelect and OnDeselect. Unity's
    /// stupid inputField won't let me do this on Awake() as there are no events
    /// for them. 

    private void LockPlayer()
    {
        BoltEntity player = ControlledEntity.controlledEntity;
        PlayerInputOrchestrator movementOrchestrator = player.GetComponent<PlayerInputOrchestrator>();
        movementOrchestrator.OverrideStop(true);
    }

    public void UnlockPlayer()
    {
        BoltEntity player = ControlledEntity.controlledEntity;
        PlayerInputOrchestrator movementOrchestrator = player.GetComponent<PlayerInputOrchestrator>();
        movementOrchestrator.OverrideStop(false);
    }
}
