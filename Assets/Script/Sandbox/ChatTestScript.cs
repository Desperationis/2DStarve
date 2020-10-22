using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Bolt;

public class Message
{
    public string message;
}


public class ChatTestScript : Bolt.GlobalEventListener
{
    List<Message> messages = new List<Message>();

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

    private void SendBoltMessage(string message) {
        ChatMessageEvent chatMessageEvent = ChatMessageEvent.Create();
        chatMessageEvent.Message = message;
        chatMessageEvent.Send();
    }

    private void CreateMessage(string message)
    {
        Message newMessage = new Message();
        newMessage.message = message;

        messages.Add(newMessage);

        GameObject newTextMessage = GameObject.Instantiate(chatTextPrefab, content);
        TextMeshProUGUI TMPtext = newTextMessage.GetComponent<TextMeshProUGUI>();

        TMPtext.text = message;
    }


    public override void OnEvent(ChatMessageEvent evnt)
    {
        CreateMessage(evnt.Message);
    }


}
