using Bolt;

public abstract class MovementOrchestrator<T> : NetworkOrchestrator<T>
{
    /// <summary>
    /// Callback function that should update the direction the mob is heading.
    /// </summary>
    protected abstract void DirectionUpdate();

    /// <summary>
    /// Callback function that should update whether or not the mob is running.
    /// </summary>
    protected abstract void RunningUpdate();
}
