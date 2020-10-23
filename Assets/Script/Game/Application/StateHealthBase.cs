using UnityEngine.Events;

/// <summary>
/// A base class that safely exposes the health component of a 
/// Bolt Entity's state while supportin client-side prediction. 
/// </summary>
public abstract class StateHealthBase<T> : Bolt.EntityBehaviour<T>
{
    public bool IsDead { get { return GetStateHealth() < 0; } }

    /// <summary>
    /// Variable that holds a local copy of the health state. This is done
    /// so that attacking looks instantaneous. 
    /// </summary>
    protected int _localHealth = 0;

    public class HealthEvent : UnityEvent<int> { }

    public HealthEvent onHealthChange = new HealthEvent();

    public override void Attached()
    {
        InitializeHealth();
    }

    public void TakeDamage(int amount)
    {
        SetStateHealth(GetStateHealth() - amount);
    }



    /// <summary>
    /// This should implement a callback to HealthUpdate() and 
    /// initialize _localHealth.
    /// </summary>
    public abstract void InitializeHealth();


    /// <summary>
    /// A callback function that updates the local copy of health
    /// with the server. 
    /// </summary>
    protected void HealthUpdate()
    {
        _localHealth = _GetStateHealth();
        onHealthChange.Invoke(GetStateHealth());
    }


    /// <summary>
    /// Returns either the local copy or actual value of health depending 
    /// if the application is a server or client. 
    /// </summary>
    public int GetStateHealth()
    {
        if(BoltNetwork.IsServer)
        {
            return _GetStateHealth();
        }

        return _localHealth;
    }


    /// <summary>
    /// Private method that returns the raw value 
    /// </summary>
    protected abstract int _GetStateHealth();


    /// <summary>
    /// Private method that allows the base class to change the health of the 
    /// state. A safer version is implemented in SetStateHealth(int).
    /// </summary>
    protected abstract void _SetStateHealth(int health);


    /// <summary>
    /// Changes the health of this mob safely. 
    /// </summary>
    public void SetStateHealth(int health)
    {
        if(BoltNetwork.IsServer)
        {
            _SetStateHealth(health);
        }
        else
        {
            _localHealth = health;
            onHealthChange.Invoke(GetStateHealth());
        }
    }
}
