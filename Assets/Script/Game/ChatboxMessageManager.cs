using UnityEngine;
using TMPro;

/// <summary>
/// A class that deals with requesting and viewing chatbox messages. Must be put
/// into the "Content" gameObject of the chatbox.
/// </summary>
public class ChatboxMessageManager : Bolt.GlobalEventListener
{
    private GameObject chatTextPrefab = null;

    private void Awake()
    {
        chatTextPrefab = (GameObject)Resources.Load("ChatText");
    }

    /// <summary>
    /// Raise a ChatMessageRequestEvent containing a chat messsage to the server. 
    /// This is usually attached to an input field.
    /// </summary>
    public void RequestMessage(string message)
    {
        ChatMessageRequestEvent chatMessageEvent = ChatMessageRequestEvent.Create();
        chatMessageEvent.Message = message;
        chatMessageEvent.Send();
    }

    /// <summary>
    /// Creates a text prefab as a child of this gameObject for it to
    /// be seen in chat.
    /// </summary>
    private void InstantiateTextPrefab(string message)
    {
        GameObject newTextMessage = GameObject.Instantiate(chatTextPrefab, transform);
        TextMeshProUGUI TMPtext = newTextMessage.GetComponent<TextMeshProUGUI>();
        TMPtext.text = message;
    }


    public override void OnEvent(ChatMessageEvent evnt)
    {
        // Display any and all messages approved by server.
        InstantiateTextPrefab(evnt.Message);
    }
}
