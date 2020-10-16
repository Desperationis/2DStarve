using UnityEngine;

/// <summary>
/// Fun little script that exposes the variables of IPlayerState's 
/// synces variables; Allows the Unity Editor to interveine. 
/// </summary>
public class PlayerServerSpeedController : Bolt.EntityBehaviour<IPlayerState>
{
    [SerializeField]
    private float requestedSpeed = 0.0f;

    [SerializeField]
    private float requestedRunningMultiplier = 0.0f;

    [ContextMenu("Save Changes")]
    private void SaveChanges()
    {
        if(BoltNetwork.IsServer)
        {
            state.Speed = requestedSpeed;
            state.RunningMultiplier = requestedRunningMultiplier;
        }
        else
        {
            Debug.LogWarning("PlayerServerSpeedController.cs: Something went wrong saving changes.");
        }
    }

}
