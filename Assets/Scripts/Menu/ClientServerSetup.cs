using System;
using UnityEngine;
using Bolt.Matchmaking;
using UdpKit;

/// <summary>
/// A script that is called by buttons to set up the application
/// as either a client or a server. 
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
