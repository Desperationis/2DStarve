using UnityEngine;
using Bolt;

public class MobServerVariableController : Bolt.EntityBehaviour<IMobState>
{
    [SerializeField]
    private int requestedHealth = 0;


    [ContextMenu("Get Current State")]
    private void GetCurrentState()
    {
        requestedHealth = state.Health;
    }


    [ContextMenu("Save Changes")]
    private void SaveChanges()
    {
        if (BoltNetwork.IsServer)
        {
            state.Health = requestedHealth;
        }
        else
        {
            Debug.LogWarning("MobServerVariableController.cs: Something went wrong saving changes.");
        }
    }
}
