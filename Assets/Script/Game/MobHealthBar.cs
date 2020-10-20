using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MobHealthBar : MonoBehaviour
{
    [SerializeField]
    [ReadOnly]
    private int health;

    [SerializeField]
    private TextMeshPro healthBar;

    public void SetHealth(int health)
    {
        this.health = health;
        healthBar.text = string.Format("Health: {0}", health);
    }
}
