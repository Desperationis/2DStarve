using Bolt;

/// <summary>
/// A script that deals with callbacks on the GameScene when
/// the application is configured to be a server. 
/// </summary>
[BoltGlobalBehaviour(BoltNetworkModes.Server, "GameScene")]
public class ServerCallbacks : Bolt.GlobalEventListener
{
    private void Start()
    {
        // Comment this line out if you don't want a player for the server
        PlayerRegistry.CreateServerPlayer();
    }

    public override void Connected(BoltConnection connection)
    {
        PlayerRegistry.CreateClientPlayer(connection);
    }

    public override void Disconnected(BoltConnection connection)
    {
        PlayerRegistry.DestroyPlayer(connection);
    }

    public override void SceneLoadLocalDone(string scene, IProtocolToken token)
    {
        PlayerRegistry.ServerPlayer.Spawn();
    }

    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
    {
        // When a client is done loading on their end, spawn it. 
        PlayerRegistry.GetPlayer(connection).Spawn();
    }

    public override void ControlOfEntityGained(BoltEntity entity)
    {
        // Follow server-controlled player around
        if (entity.StateIs<IPlayerState>())
        {
            PlayerCamera.Instance.Follow(entity);
        }
    }
}
