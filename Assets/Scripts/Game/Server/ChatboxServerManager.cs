/// <summary>
/// A server-only class that verifies and sends back messages to clients.
/// This is mainly intenteded to be the place where commands or private messages
/// are only sent to a specific person or group of people.
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
