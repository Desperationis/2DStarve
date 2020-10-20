using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHealthSetter : Bolt.EntityBehaviour<IMobState>
{
    public int StateHealth { get { return state.Health; } }

    public void SetStateHealth(int health)
    {
        if(BoltNetwork.IsServer)
        {
            state.Health = health;
        }
    }
}
