using UnityEngine;
using TMPro;

/// <summary>
/// Class dedicated to listening for messages from the server and putting them
/// into the  chatbox.
/// </summary>
public class ChatboxMessageOrchestrator : Bolt.GlobalEventListener
{
    [SerializeField]
    private Transform contentPanel = null;

    private GameObject chatTextPrefab = null;

    private void Awake()
    {
        chatTextPrefab = (GameObject)Resources.Load("ChatText");
    }

    public override void OnEvent(ChatMessageEvent evnt)
    {
        // Display any and all messages approved by server.
        InstantiateTextPrefab(evnt.Message);
    }

    /// <summary>
    /// Creates a text prefab as a child of this gameObject for it to be seen in
    /// chat.
    /// </summary>
    private void InstantiateTextPrefab(string message)
    {
        GameObject newTextMessage = GameObject.Instantiate(chatTextPrefab, contentPanel);
        TextMeshProUGUI TMPtext = newTextMessage.GetComponent<TextMeshProUGUI>();
        TMPtext.text = message;
    }
}
