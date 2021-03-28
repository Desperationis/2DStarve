using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobDeathComponent : Bolt.EntityBehaviour<IMobState>
{
    [SerializeField]
    private HealthComponent health = null;

    private void Awake()
    {
        health.AddListener(CheckHealth);
    }

    private void CheckHealth(int health)
    {
        if(health <= 0 && BoltNetwork.IsServer)
        {
            Destroy(gameObject);
        }
    }
}
