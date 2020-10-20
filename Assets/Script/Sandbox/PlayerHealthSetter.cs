using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSetter : Bolt.EntityBehaviour<IPlayerState>
{
    public int StateHealth { get { return state.Health; } }

    public void SetStateHealth(int health)
    {
        if (BoltNetwork.IsServer)
        {
            state.Health = health;
        }
    }
}
