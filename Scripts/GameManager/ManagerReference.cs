using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public interface IManagerReference
{
    void SetManagerReference(ManagerReference managerReference);
    void SetLocalPlayerReference(PlayerReference playerReference);
}

public class ManagerReference : MonoBehaviourPun
{
    private GameManager gameManager;
    private ChatManager chatManager;
    private PlayerReference localPlayerReference;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        gameManager.SetManagerReference(this);
        chatManager = GetComponent<ChatManager>();
        chatManager.SetManagerReference(this);
    }

    public GameManager GetGameManager()
    {
        return gameManager;
    }

    public ChatManager GetChatManager()
    {
        return chatManager;
    }

    public PlayerReference GetLocalPlayerReference()
    {
        return localPlayerReference;
    }

    public void SetLocalPlayerReference(PlayerReference playerReference)
    {
        localPlayerReference = playerReference;
    }
}
