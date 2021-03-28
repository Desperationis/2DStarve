using UnityEngine;

/// <summary>
/// Base class that synchs the HealthComponent of  a mob.
/// </summary>
public abstract class HealthOrchestrator<T> : MobBehaviour<T>
{
    [SerializeField]
    private HealthComponent healthComponent = null;

    public override void Attached()
    {
        LinkHealth();

        if (BoltNetwork.IsServer)
        {
            healthComponent.SetHealth(100); // TODO: Move this line outta here
            SetStateHealth(healthComponent.health);
            healthComponent.AddListener((int x) => { UpdateHealth(); });
        }
    }

    /// <summary>
    /// This should link UpdateHealth to the Health variable of a bolt entity
    /// via state.AddCallback().
    /// </summary>
    public abstract void LinkHealth();

    /// <summary>
    /// Private method that returns the raw value
    /// </summary>
    protected abstract int GetStateHealth();


    /// <summary>
    /// Private method that allows the base class to change the health of the
    /// state. A safer version is implemented in SetStateHealth(int).
    /// </summary>
    protected abstract void SetStateHealth(int health);


    protected void UpdateHealth()
    {
        if(BoltNetwork.IsClient)
        {
            healthComponent.SetHealth(GetStateHealth());
        }
        else
        {
            SetStateHealth(healthComponent.health);
        }
    }
}
