using System;
using UnityEngine;
using Bolt.Matchmaking;
using UdpKit;

/// <summary>
/// Interface for menu buttons to start up the game as either a server // or
/// client.
/// </summary>
public class ClientServerSetup : Bolt.GlobalEventListener
{
    public void StartClient()
    {
        BoltLauncher.StartClient();
    }

    public void StartServer()
    {
        BoltLauncher.StartServer();
    }

    public override void BoltStartDone()
    {
        // Create a session when Bolt is done setting up
        if (BoltNetwork.IsServer)
        {
            string matchName = "Test Match";
            string scene = "GameScene";

            // Fill out parameters by name
            BoltMatchmaking.CreateSession(
                sessionID: matchName,
                sceneToLoad: scene
            );
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        Debug.LogFormat("Session list updated: {0} total sessions", sessionList.Count);

        // Join any available room
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value;

            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltMatchmaking.JoinSession(photonSession);
            }
        }
    }
}
