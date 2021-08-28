using UnityEngine;

/// <summary>
/// Abstract representation of a player that allows for  easy creation,
/// referencing, and destruction. This even allows the server application to
/// play as player.
/// </summary>
public class PlayerObject
{
    public BoltEntity character { get; private set; }

    private BoltConnection connection = null;

    public bool IsServer
    {
        get { return connection == null; }
    }

    public bool IsClient
    {
        get { return connection != null; }
    }

    /// <summary>
    /// Lookup the PlayerObject reference this connection is assigned
    /// to in constant time (actually just returns a internal reference)
    /// </summary>
    /// <param name="connection">A refernece to the connection</param>
    /// <returns>The player object, null if not found.</returns>
    public static PlayerObject LookupConnection(BoltConnection connection)
    {
        if(connection == null)
        {
            return null;
        }

        return (PlayerObject)connection.UserData;
    }

    public void AssignToConnection(BoltConnection connection)
    {
        if(connection != null)
        {
            if(this.connection != null)
            {
                // The last connection is no longer assigned to this character.
                this.connection.UserData = null;
            }

            this.connection = connection;

            if (IsClient)
            {
                this.connection.UserData = this; // Put this object inside child for constant-time lookup
            }
        }
    }

    /// <summary>
    /// Spawn at the oldest known spawn point.
    /// </summary>
    public void Spawn()
    {
        if(!character)
        {
            character = BoltNetwork.Instantiate(BoltPrefabs.Player, SpawnRegistry.GetSpawnPoint(), Quaternion.identity);

            if(IsServer)
            {
                character.TakeControl();
            }
            else
            {
                character.AssignControl(connection);
            }
        }

        character.transform.position = SpawnRegistry.GetSpawnPoint();
    }

    /// <summary>
    /// Leave no artifacts if / when a client disconnects.
    /// </summary>
    public void BoltDestroy()
    {
        if(character)
        {
            BoltNetwork.Destroy(character);
        }
    }
}
