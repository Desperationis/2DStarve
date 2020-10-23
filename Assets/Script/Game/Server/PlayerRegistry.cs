using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A static class that deals with finding and creating players in a 
/// registry; Only applies to the server.
/// </summary>
public static class PlayerRegistry
{
    static private List<PlayerObject> players = new List<PlayerObject>();

    /// <summary>
    /// Creates a PlayerObject in the registry given a BoltConnection. This does
    /// not spawn the player, and can only be done in singleplayer and server applications. 
    /// </summary>
    static public PlayerObject CreatePlayer(BoltConnection connection)
    {
        if(BoltNetwork.IsServer || BoltNetwork.IsSinglePlayer)
        {
            PlayerObject player;

            // Connect a new player instance with a connection;
            player = new PlayerObject();
            player.connection = connection;

            // Put clients into an internal variable for easy access via BoltConnection
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

    /// <summary>
    /// Finds the PlayerObject that the server application is currently
    /// using. If it doesn't exist, it returns null. 
    /// </summary>
    public static PlayerObject ServerPlayer
    {
        get { return players.Find(player => player.IsServer); }
    }

    /// <summary>
    /// Creates a PlayerObject in the registry using CreatePlayer(BoltConnection). This 
    /// PlayerObject references the player the server is using. 
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
    /// Tries to destroy an instance of a player given its connection. A value of
    /// null will delete the server player. 
    /// </summary>
    public static void DestroyPlayer(BoltConnection connection)
    {
        if (BoltNetwork.IsServer || BoltNetwork.IsSinglePlayer)
        {
            PlayerObject playerInQuestion = GetPlayer(connection);

            if(playerInQuestion != null)
            {
                playerInQuestion.BoltDestroy();
                players.Remove(playerInQuestion);
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

        // Easy access!
        return (PlayerObject)connection.UserData;
    }


    /// <summary>
    /// Returns all the overlapping players that fall within a given circle. Returns an 
    /// empty list if none were found.
    /// </summary>
    public static List<PlayerObject> OverlapCircleAll(Vector2 point, float radius)
    {
        List<PlayerObject> foundPlayers = new List<PlayerObject>();

        foreach(PlayerObject player in AllPlayers)
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
        foreach (PlayerObject player in AllPlayers)
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
    /// Returns the closest overlapping player to a center of a circle. Returns null
    /// if a player wasn't found. 
    /// </summary>
    public static PlayerObject OverlapCircle(Vector2 point, float radius)
    {
        PlayerObject closestPlayer = null;
        Vector2 closestDifference = Vector2.positiveInfinity;

        foreach (PlayerObject player in AllPlayers)
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