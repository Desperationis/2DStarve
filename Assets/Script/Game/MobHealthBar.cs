using UnityEngine;
using TMPro;

/// <summary>
/// Displays a health bar by listening to value changes in a
/// StateHealthBase derived class. 
/// </summary>
public class MobHealthBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro healthBar = null;

    [SerializeField]
    private PlayerStateHealth playerStateHealth = null;

    [SerializeField]
    private MobStateHealth mobStateHealth = null;

    private void Awake()
    {
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
