using UnityEngine;
using TMPro;

public class MobHealthBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro healthBar;

    [SerializeField]
    private PlayerStateHealth playerStateHealth;

    [SerializeField]
    private MobStateHealth mobStateHealth;

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
