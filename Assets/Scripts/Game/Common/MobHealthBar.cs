using UnityEngine;
using TMPro;

/// <summary>
/// Displays a health bar by listening to value changes in a
/// StateHealthBase derived class. 
/// </summary>
public class MobHealthBar : MobBehaviour
{
    [SerializeField]
    private TextMeshPro healthBar = null;

    [SerializeField]
    private PlayerHealthOrchestrator playerStateHealth = null;

    [SerializeField]
    private MobHealthOrchestrator mobStateHealth = null;

    protected void Awake()
    {
        base.Awake();
        if(playerStateHealth != null)
        {
            playerStateHealth.onHealthChange.AddListener(SetHealth);
        }
        else if (mobStateHealth != null)
        {
            mobStateHealth.onHealthChange.AddListener(SetHealth);
        }
    }

    private void SetHealth(int health)
    {
        healthBar.text = string.Format("Health: {0}", health);
    }
}
