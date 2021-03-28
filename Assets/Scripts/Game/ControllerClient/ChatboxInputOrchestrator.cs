using UnityEngine;

/// <summary>
/// Class dedicated to sending client's chatbox input to the server.
/// </summary>
public class ChatboxInputOrchestrator : MonoBehaviour
{
    [SerializeField]
    private InputFieldExpander inputField = null;

    private void Awake()
    {
        inputField.AddOnSubmitListener(RequestMessage);
    }

    /// <summary>
    /// Raise a ChatMessageRequestEvent containing a chat messsage to the
    /// server.  This is usually attached to an input field.
    /// </summary>
    public void RequestMessage(string message)
    {
        ChatMessageRequestEvent chatMessageEvent = ChatMessageRequestEvent.Create();
        chatMessageEvent.Message = message;
        chatMessageEvent.Send();
    }
}
