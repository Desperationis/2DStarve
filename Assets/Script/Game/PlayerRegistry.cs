using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class that deals with finding and creating players in a 
/// registry; Only applies to the server.
/// </summary>
public static class PlayerRegistry
{
    static private List<PlayerObject> players = new List<PlayerObject>();

    static public PlayerObject CreatePlayer(BoltConnection connection)
    {
        if(BoltNetwork.IsServer || BoltNetwork.IsSinglePlayer)
        {
            PlayerObject player;

            // Connect a new player instance with a connection;
            player = new PlayerObject();
            player.connection = connection;

            // Put clients into an internal variable for easy access later
            if (player.IsClient)
            {
                player.connection.UserData = player;
            }

            players.Add(player);

            return player;
        }

        Debug.LogError("PlayerRegistry.cs: You can only create players if you are the server.");
        return null;
    }

    public static IEnumerable<PlayerObject> AllPlayers
    {
        get { return players; }
    }

    public static PlayerObject ServerPlayer
    {
        get { return players.Find(player => player.IsServer); }
    }

    public static PlayerObject CreateServerPlayer()
    {
        return CreatePlayer(null);
    }

    public static PlayerObject CreateClientPlayer(BoltConnection connection)
    {
        return CreatePlayer(connection);
    }

    public static void DestroyPlayer(BoltConnection connection)
    {
        if (BoltNetwork.IsServer || BoltNetwork.IsSinglePlayer)
        {
            GetPlayer(connection).BoltDestroy();
            players.Remove(GetPlayer(connection));
        }
        else
        {
            Debug.LogWarning("PlayerRegistry.cs: You can only destroy players if you are the server!");
        }
    }

    public static PlayerObject GetPlayer(BoltConnection connection)
    {
        if (connection == null)
        {
            return ServerPlayer;
        }

        // Easy access!
        return (PlayerObject)connection.UserData;
    }
}