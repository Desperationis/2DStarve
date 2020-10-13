using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

/// <summary>
/// Abstract representation of a player. This allows the server
/// to exist in the game without a Bolt Connection. 
/// </summary>
public class PlayerObject
{
    public BoltEntity character;
    public BoltConnection connection;

    public void Spawn()
    {
        // If the character doesn't exist, create one. 
        if(!character)
        {
            character = BoltNetwork.Instantiate(BoltPrefabs.Player, Vector3.zero, Quaternion.identity);

            if(IsServer)
            {
                character.TakeControl();
            }
            else
            {
                character.AssignControl(connection);
            }
        }

        character.transform.position = Vector3.zero;
    }

    public bool IsServer
    {
        get { return connection == null; }
    }

    public bool IsClient
    {
        get { return connection != null; }
    }
}
