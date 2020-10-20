using UnityEngine;

/// <summary>
/// Fun little script that exposes the variables of IPlayerState's 
/// synced variables; Allows the Unity Editor to interveine. 
/// </summary>
public class PlayerServerVariableController : Bolt.EntityBehaviour<IPlayerState>
{
    [SerializeField]
    private float requestedSpeed = 0.0f;

    [SerializeField]
    private float requestedRunningMultiplier = 0.0f;

    [SerializeField]
    private int requestedHealth = 0;


    [ContextMenu("Get Current State")]
    private void GetCurrentState()
    {
        requestedSpeed = state.Speed = requestedSpeed;
        requestedRunningMultiplier = state.RunningMultiplier;
        requestedHealth = state.Health;
    }



    [ContextMenu("Save Changes")]
    private void SaveChanges()
    {
        if(BoltNetwork.IsServer)
        {
            state.Speed = requestedSpeed;
            state.RunningMultiplier = requestedRunningMultiplier;
            state.Health = requestedHealth;
        }
        else
        {
            Debug.LogWarning("PlayerServerVariableController.cs: Something went wrong saving changes.");
        }
    }

}
