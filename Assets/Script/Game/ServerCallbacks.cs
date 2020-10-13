using UnityEngine;
using Bolt;

/// <summary>
/// A script that deals with callbacks on the GameScene when
/// the application is configured to be a server. 
/// </summary>
[BoltGlobalBehaviour(BoltNetworkModes.Server, "GameScene")]
public class ServerCallbacks : Bolt.GlobalEventListener
{
    private void Awake()
    {
        PlayerRegistry.CreateServerPlayer();
    }

    public override void Connected(BoltConnection connection)
    {
        PlayerRegistry.CreateClientPlayer(connection);
    }

    public override void SceneLoadLocalDone(string scene)
    {
        PlayerRegistry.ServerPlayer.Spawn();
    }

    public override void SceneLoadRemoteDone(BoltConnection connection)
    {
        // When a client is done loading, spawn it. 
        PlayerRegistry.GetPlayer(connection).Spawn();
    }
}
