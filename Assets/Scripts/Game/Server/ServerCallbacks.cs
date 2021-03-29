using Bolt;
using UnityEngine;
using System.Collections;

/// <summary>
/// A script that deals with callbacks on the GameScene when the application is
/// configured to be a server.
/// </summary>
public class ServerCallbacks : Bolt.GlobalEventListener
{
    [SerializeField]
    private MapLoader map = null;

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
        StartCoroutine("SpawnPlayer", PlayerRegistry.ServerPlayer);
    }

    public override void SceneLoadRemoteDone(BoltConnection connection, IProtocolToken token)
    {
        // When a client is done loading on their end, spawn it.
        StartCoroutine("SpawnPlayer", PlayerRegistry.GetPlayer(connection));
    }

    /// <summary>
    /// Spawns the player at spawn. If a map is not loaded
    /// yet, keep retrying every second.
    /// </summary>
    private IEnumerator SpawnPlayer(PlayerObject player)
    {
        while(map.GetCurrentMap() == null)
        {
            yield return new WaitForSeconds(1);
        }

        Vector3 spawnPoint = MapEntitySearcher.GetSpawnPoint(map.GetCurrentMap());
        player.Spawn(spawnPoint);
    }
}
