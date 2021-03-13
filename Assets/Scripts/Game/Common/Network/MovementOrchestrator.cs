using Bolt;

/// <summary>
/// Base class for movement orchestrators. Before any of that network
/// stuff happens, this initializes the speed of MobController via 
/// a data component.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MovementOrchestrator<T> : NetworkOrchestrator<T>
{
    public void Start()
    {
        mobController.SetSpeed(dataComponent.movementData.speed);
        mobController.SetRunningMultiplier(dataComponent.movementData.runningMultiplier);
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
