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
    private HealthComponent healthComponent = null;

    protected override void Awake()
    {
        base.Awake();
        healthComponent.AddListener(SetDisplayHealth);
        SetDisplayHealth(healthComponent.health);
    }

    private void SetDisplayHealth(int health)
    {
        healthBar.text = string.Format("Health: {0}", health);
    }
}
