using Bolt;
using UnityEngine;

/// <summary>
/// Base class for movement orchestrators. Before any of that network stuff
/// happens, this initializes the speed of MobController via  a data component.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MovementOrchestrator<T> : NetworkOrchestrator<T>
{
    [SerializeField]
    [Tooltip("Used to prevent jiggle in non-controlling clients.")]
    private Rigidbody2D rigidBody = null;

    public void Start()
    {
        mobController.SetSpeed(dataComponent.movementData.speed);
        mobController.SetRunningMultiplier(dataComponent.movementData.runningMultiplier);
    }

    public void Update()
    {
        // This piece of code is crucial. On the client side, mobs
        // will jiggle if touched as velocity is not synced. On the 
        // server side, velocity must never be set to 0 as it has 
        // authoritative control. This code fixes the jiggle client side
        // while leaving the server untouched.
        if(!entity.IsControllerOrOwner)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }


    /// <summary>
    /// Callback function that should update the direction the mob is heading.
    /// </summary>
    protected abstract void DirectionUpdate();

    /// <summary>
    /// Callback function that should update whether or not the mob is running.
    /// </summary>
    protected abstract void RunningUpdate();
}
