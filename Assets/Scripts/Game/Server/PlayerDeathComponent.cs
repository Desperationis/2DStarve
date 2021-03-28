using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathComponent : Bolt.EntityBehaviour<IPlayerState>
{
    [SerializeField]
    private HealthComponent health = null;

    private void Awake()
    {
        health.AddListener(CheckHealth);
    }

    private void CheckHealth(int healthValue)
    {
        if (healthValue <= 0 && BoltNetwork.IsServer)
        {
            StartCoroutine("ChangeHealthNextFrame", 100);
            transform.position = Vector3.zero;
        }
    }

    private IEnumerator ChangeHealthNextFrame(int healthValue)
    {
        yield return new WaitForEndOfFrame();
        health.SetHealth(100);
    }
}
