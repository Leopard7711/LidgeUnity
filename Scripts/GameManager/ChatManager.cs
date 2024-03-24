using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPun, IChatClientListener, IManagerReference
{
    private ManagerReference managerReference;
    private GameManager gameManager;    
    private PlayerReference localPlayerReference;
    private ChatClient chatClient;
    private string appId ="65f9e646-21c8-40ca-a1af-2db82d5e226f";
    public InputField chatInputField;

    private bool isFocused;

    private void Start()
    {
        gameManager = managerReference.GetGameManager();
        GameManager.actionBeforeLeavesRoom += LeaveRoom; 
        
        chatClient = new ChatClient(this);
        chatClient.ChatRegion = "asia";
        chatClient.Connect(appId, "1.0",new Photon.Chat.AuthenticationValues(PhotonNetwork.LocalPlayer.NickName));
        
    }

    

    private void Update()
    {
        chatClient.Service();
        
        if (isFocused)
        {
            if (chatInputField.isFocused == false)
            {
                localPlayerReference.GetPlayerInput().BlockInput(false);  
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
           SendMessage(); 
        }
    }

    public void BlockInput()
    {
        isFocused = true;
        localPlayerReference = managerReference.GetLocalPlayerReference();
        localPlayerReference.GetPlayerInput().BlockInput(true);        
        
        
    }
    
    
    public void SendMessage()
    {
        
        
        
        string messageToSend = chatInputField.text;
        print("SendMessage: " + messageToSend);
        chatInputField.text = "";
        if (!string.IsNullOrEmpty(messageToSend))
        {
            bool _result= chatClient.PublishMessage("MainChat", messageToSend);
            if (_result)
            {
                localPlayerReference.GetPhotonView().RPC("RegisterChat",RpcTarget.All,messageToSend);
            }
        }
        
    }

    private void LeaveRoom()
    {
        if (localPlayerReference != null)
        {
            localPlayerReference.GetPhotonView().RPC("ShutDownChat",RpcTarget.All);
        }
        
    }
   
    public void DebugReturn(DebugLevel level, string message)
    { 
        
    }
    

    public void OnDisconnected()
    {
        print("Chat Disconnected");
    }

    public void OnConnected()
    {
        print("Chat Connected");
        chatClient.Subscribe(new []{"MainChat"},10);
    }

    public void OnChatStateChange(ChatState state)
    {
       
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        print("OnGetMessages");
        string msgs = "";
        for ( int i = 0; i < senders.Length; i++ )
        {
            msgs += senders[i] + "=" + messages[i] + ", ";

        }
        Debug.Log( "OnGetMessages: " + channelName + "(" + senders.Length + ") > " + msgs );
        
        
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        ChatChannel ch = chatClient.PrivateChannels[ channelName ];
        foreach ( object msg in ch.Messages )
        {
            Console.WriteLine( msg );
        }
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        print("Subscribed! : "+channels);
    }

    public void OnUnsubscribed(string[] channels)
    {
        print("UnSubscribed! : "+channels);
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        //print(user+" : "+status);
    }

    public void OnUserSubscribed(string channel, string user)
    {
        print("UserSubscribed! : "+channel);
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        print("UserUnSubscribed! : "+channel);
    }
    
    public void SetManagerReference(ManagerReference managerReference)
    {
        this.managerReference = managerReference;
    }

    public void SetLocalPlayerReference(PlayerReference playerReference)
    {
        localPlayerReference = playerReference;
    }

    public void OnApplicationQuit ()
    {
        if (chatClient != null)
        {
            chatClient.Disconnect();
        }
    }
    
}


