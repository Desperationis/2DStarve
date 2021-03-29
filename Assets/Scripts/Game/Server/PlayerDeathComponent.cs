using System.Collections;
using UnityEngine;

/// <summary>
/// Component resposible for the death of a player. 
/// </summary>
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
            PlayerObject player = PlayerRegistry.GetPlayer(entity);
            if(player != null)
            {
                player.Spawn();
            }
        }
    }

    private IEnumerator ChangeHealthNextFrame(int healthValue)
    {
        yield return new WaitForEndOfFrame();
        health.SetHealth(healthValue);
    }
}
