using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Component that controls health of an entity locally.
/// </summary>
public class HealthComponent : MobBehaviour
{
    /// <summary>
    /// Variable that holds a local copy of the health state. 
    /// </summary>
    public int health { get; private set; }

    private class HealthEvent : UnityEvent<int> { }

    private HealthEvent onHealthChange = new HealthEvent();


    public void TakeDamage(int amount)
    {
        SetHealth(health - amount);
    }


    /// <summary>
    /// Changes the health of this mob. This will invoke onHealthChange.
    /// </summary>
    public void SetHealth(int health)
    {
        int pastHealth = this.health;
        this.health = Mathf.Min(100, Mathf.Max(health, 0));

        if(pastHealth != this.health)
        {
            onHealthChange.Invoke(this.health);
        }
    }

    /// <summary>
    /// Adds a listener to onHealthChange.
    /// </summary>
    /// <param name="call"></param>
    public void AddListener(UnityAction<int> call)
    {
        onHealthChange.AddListener(call);
    }
}
