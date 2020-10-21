using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A base class that safely exposes the health component of a 
/// Bolt Entity's state.
/// </summary>
[System.Serializable]
public class StateHealthBase<T> : Bolt.EntityBehaviour<T>
{
    public bool IsDead { get { return GetStateHealth() < 0; } }

    public class HealthEvent : UnityEvent<int> { }

    public HealthEvent onHealthChange = new HealthEvent();

    public override void Attached()
    {
        if(BoltNetwork.IsServer)
        {
            SetStateHealth(100);
        }
    }


    protected void HealthUpdate()
    {
        onHealthChange.Invoke(GetStateHealth());
    }

    /// <summary>
    /// Returns the health of the mob via its state. This method should be implemented in children. 
    /// </summary>
    public virtual int GetStateHealth()
    {
        return 0;
    }

    /// <summary>
    /// Private method that allows the base class to change the health of the 
    /// state. A safer version is implemented in SetStateHealth(int).
    /// </summary>
    protected virtual void _SetStateHealth(int health)
    {

    }

    /// <summary>
    /// Changes the health of this mob safely. 
    /// </summary>
    public void SetStateHealth(int health)
    {
        if(BoltNetwork.IsServer || BoltNetwork.IsSinglePlayer)
        {
            _SetStateHealth(health);
        }
        else
        {
            Debug.LogError("HealthBaseBehavior.cs: Client cannot change health of a mob.");
        }
    }
}
