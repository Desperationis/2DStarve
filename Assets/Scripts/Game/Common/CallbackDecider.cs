using UnityEngine;

/// <summary>
/// Decides which callback is enabled depending on
/// the current mode.
/// </summary>
public class CallbackDecider : MonoBehaviour
{
    [SerializeField]
    private ServerCallbacks serverCallback = null;
    
    [SerializeField]
    private ClientCallbacks clientCallback = null;

    private void Awake()
    {
        if(BoltNetwork.IsServer)
        {
            serverCallback.enabled = true;
        }
        if (BoltNetwork.IsClient)
        {
            clientCallback.enabled = true;
        }
    }
}
