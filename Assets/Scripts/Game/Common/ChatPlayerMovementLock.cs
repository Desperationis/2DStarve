﻿using UnityEngine;

/// <summary>
/// Locks the currently controlled player in position while typing by completely
/// disabling the player from sending input commands.
/// </summary>
public class ChatPlayerMovementLock : MonoBehaviour
{
    public void LockPlayer()
    {
        BoltEntity player = ControlledEntity.controlledEntity;
        PlayerMovementOrchestrator movementOrchestrator = player.GetComponent<PlayerMovementOrchestrator>();
        movementOrchestrator.OverrideStop(true);
    }

    public void UnlockPlayer()
    {
        BoltEntity player = ControlledEntity.controlledEntity;
        PlayerMovementOrchestrator movementOrchestrator = player.GetComponent<PlayerMovementOrchestrator>();
        movementOrchestrator.OverrideStop(false);
    }
}
