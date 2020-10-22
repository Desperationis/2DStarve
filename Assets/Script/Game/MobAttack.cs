using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    [SerializeField]
    private MobStateHealth mobHealth;

    /// <summary>
    /// A callback function to a gameObject message.
    /// </summary>
    public void TakeDamage(int amount)
    {
        mobHealth.SetStateHealth(mobHealth.GetStateHealth() - amount);
    }
}
