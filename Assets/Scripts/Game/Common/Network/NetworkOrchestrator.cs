using UnityEngine;

/// <summary>
/// Base class for anything that needs to be synched over
/// Photon Bolt.
/// </summary>
/// <typeparam name="T">Interface for the entity.</typeparam>
public class NetworkOrchestrator<T> : Bolt.EntityEventListener<T>
{
    protected MobController mobController = null;

    private void Awake()
    {
        mobController = GetComponent<MobController>();
        _Awake();
    }

    private void Start()
    {
        if(mobController == null)
        {
            mobController = GetComponent<MobController>();
        }
        _Start();
    }

    protected virtual void _Awake()
    {

    }

    protected virtual void _Start()
    {

    }
}
