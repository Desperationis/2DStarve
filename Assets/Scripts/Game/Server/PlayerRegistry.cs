using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class that deals with finding and creating players in a  list; Only
/// applies to the server.
/// </summary>
public static class PlayerRegistry
{
    static private List<PlayerObject> _players = new List<PlayerObject>();

    public static IEnumerable<PlayerObject> players
    {
        get { return _players; }
    }

    /// <summary>
    /// Returns the PlayerObject that the server application is currently using.
    /// If it doesn't exist, it returns null.
    /// </summary>
    public static PlayerObject ServerPlayer
    {
        get { return _players.Find(player => player.IsServer); }
    }

    /// <summary>
    /// Creates a PlayerObject in the registry given a BoltConnection.
    /// </summary>
    static public PlayerObject CreatePlayer(BoltConnection connection)
    {
        if(BoltNetwork.IsServer)
        {
            // Connect a new player instance with a connection;
            PlayerObject player = new PlayerObject();
            player.AssignToConnection(connection);

            _players.Add(player);

            return player;
        }

        Debug.LogError("PlayerRegistry.cs: You can only create players if you are the server.");
        return null;
    }



    /// <summary>
    /// Creates a PlayerObject in the registry using
    /// CreatePlayer(BoltConnection). This  PlayerObject references the player
    /// the server is using.
    /// </summary>
    public static PlayerObject CreateServerPlayer()
    {
        return CreatePlayer(null);
    }


    /// <summary>
    /// Same functionality as CreatePlayer(BoltConnection).
    /// </summary>
    public static PlayerObject CreateClientPlayer(BoltConnection connection)
    {
        return CreatePlayer(connection);
    }

    /// <summary>
    /// Tries to destroy an instance of a player given its connection. A value
    /// of null will delete the server player.
    /// </summary>
    public static void DestroyPlayer(BoltConnection connection)
    {
        if (BoltNetwork.IsServer)
        {
            PlayerObject targetPlayer = GetPlayer(connection);

            if(targetPlayer != null)
            {
                targetPlayer.BoltDestroy();
                _players.Remove(targetPlayer);
            }
        }
        else
        {
            Debug.LogWarning("PlayerRegistry.cs: You can only destroy players if you are the server!");
        }
    }

    /// <summary>
    /// Searches for a player given its BoltConnection. If the value passed in
    /// is null, it will return the server player.
    /// </summary>
    public static PlayerObject GetPlayer(BoltConnection connection)
    {
        if (connection == null)
        {
            return ServerPlayer;
        }

        return PlayerObject.LookupConnection(connection);
    }


    /// <summary>
    /// Returns all the overlapping players that fall within a given circle.
    /// Returns an  empty list if none were found.
    /// </summary>
    public static List<PlayerObject> OverlapCircleAll(Vector2 point, float radius)
    {
        List<PlayerObject> foundPlayers = new List<PlayerObject>();

        foreach(PlayerObject player in players)
        {
            BoltEntity character = player.character;
            if(character != null)
            {
                Vector2 difference = (Vector2)character.transform.position - point;

                if(difference.sqrMagnitude <= Mathf.Pow(radius, 2))
                {
                    foundPlayers.Add(player);
                }
            }
        }

        return foundPlayers;
    }

    /// <summary>
    /// Returns whether or not any overlapping players are in a given circle.
    /// </summary>
    public static bool OverlapCircleAny(Vector2 point, float radius)
    {
        foreach (PlayerObject player in players)
        {
            BoltEntity character = player.character;
            if (character != null)
            {
                Vector2 difference = (Vector2)character.transform.position - point;

                if (difference.sqrMagnitude <= Mathf.Pow(radius, 2))
                {
                    return true;
                }
            }
        }

        return false;
    }


    /// <summary>
    /// Returns the closest overlapping player to a center of a circle. Returns
    /// null if a player wasn't found.
    /// </summary>
    public static PlayerObject OverlapCircle(Vector2 point, float radius)
    {
        PlayerObject closestPlayer = null;
        Vector2 closestDifference = Vector2.positiveInfinity;

        foreach (PlayerObject player in players)
        {
            BoltEntity character = player.character;
            if (character != null)
            {
                Vector2 difference = (Vector2)character.transform.position - point;

                if (difference.sqrMagnitude <= Mathf.Pow(radius, 2))
                {
                    if(difference.sqrMagnitude < closestDifference.sqrMagnitude || closestPlayer == null)
                    {
                        closestPlayer = player;
                        closestDifference = difference;
                    }
                }
            }
        }

        return closestPlayer;
    }
}
