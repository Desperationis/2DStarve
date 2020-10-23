using UnityEngine;

/// <summary>
/// Abstract representation of a player. This allows the server
/// to exist in the game without a Bolt Connection. Used in 
/// PlayerRegistry.cs.
/// </summary>
public class PlayerObject
{
    public BoltEntity character = null;
    public BoltConnection connection = null;

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

    public void BoltDestroy()
    {
        if(character)
        {
            BoltNetwork.Destroy(character);
        }
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
