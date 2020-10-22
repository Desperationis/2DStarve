using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// A class that deals with requesting and viewing chatbox messages. 
/// </summary>
public class ChatboxMessageManager : Bolt.GlobalEventListener
{
    private GameObject chatTextPrefab;

    [SerializeField]
    private InputFieldWrapper inputFieldWrapper;

    [SerializeField]
    private Transform content;


    private void Awake()
    {
        chatTextPrefab = (GameObject)Resources.Load("ChatText");
        inputFieldWrapper.onReturn.AddListener(SendBoltMessage);
    }

    private void SendBoltMessage(string message)
    {
        ChatMessageRequestEvent chatMessageEvent = ChatMessageRequestEvent.Create();
        chatMessageEvent.Message = message;
        chatMessageEvent.Send();
    }

    private void CreateMessage(string message)
    {
        Message newMessage = new Message();
        newMessage.message = message;

        GameObject newTextMessage = GameObject.Instantiate(chatTextPrefab, content);
        TextMeshProUGUI TMPtext = newTextMessage.GetComponent<TextMeshProUGUI>();

        TMPtext.text = message;
    }


    public override void OnEvent(ChatMessageEvent evnt)
    {
        CreateMessage(evnt.Message);
    }
}
