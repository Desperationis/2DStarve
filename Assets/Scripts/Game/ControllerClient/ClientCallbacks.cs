using UnityEngine.SceneManagement;
using Bolt;
using UdpKit;

/// <summary>
/// A script that deals with callbacks on the GameScene when the application is
/// configured to be a client.
/// </summary>
[BoltGlobalBehaviour(BoltNetworkModes.Client, "GameScene")]
public class ClientCallbacks : Bolt.GlobalEventListener
{
    public override void BoltShutdownBegin(AddCallback registerDoneCallback, UdpConnectionDisconnectReason disconnectReason)
    {
        // Go back to the menu screen
        BoltLauncher.Shutdown();
        SceneManager.LoadScene(0);
    }
}
