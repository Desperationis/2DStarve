using System.Collections.Generic;

/// <summary>
/// A class that deals with finding and creating players in a 
/// registry. 
/// </summary>
public static class PlayerRegistry
{
    static private List<PlayerObject> players = new List<PlayerObject>();

    // Create a player with a connection; Null or not. 
    public static PlayerObject CreatePlayer(BoltConnection connection)
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