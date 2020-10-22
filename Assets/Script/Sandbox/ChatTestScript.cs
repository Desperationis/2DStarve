using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Message
{
    public string message;
}


public class ChatTestScript : MonoBehaviour
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
        inputFieldWrapper.onReturn.AddListener(CreateMessage);
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



}
