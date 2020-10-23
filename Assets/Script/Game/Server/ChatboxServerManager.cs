/// <summary>
/// A server-only class that verifies and sends back messages to clients.
/// </summary>
[BoltGlobalBehaviour(BoltNetworkModes.Server, "GameScene")]
public class ChatboxServerManager : Bolt.GlobalEventListener
{
    public override void OnEvent(ChatMessageRequestEvent evnt)
    {
        ChatMessageEvent messageEvent = ChatMessageEvent.Create();
        PlayerObject raisedPlayer = PlayerRegistry.GetPlayer(evnt.RaisedBy);

        messageEvent.Message = string.Format("{0}: {1}", raisedPlayer.character.name, evnt.Message);

        messageEvent.Send();
    }
}
