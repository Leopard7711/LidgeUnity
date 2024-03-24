using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerChat : MonoBehaviourPun,IPlayerReference
{
    private PlayerReference playerReference;
    private ChatPlateControl chatPlateControl;
    private PhotonView photonView;
    void Start()
    {
        chatPlateControl = UIReference.instance.chatPlateControl;
    }


    public void SetPlayerReference(PlayerReference playerReference)
    {
        this.playerReference = playerReference;
        photonView= playerReference.GetPhotonView();
    }

    [PunRPC]
    public void RegisterChat(string chat)
    {
        EventSystem.current.SetSelectedGameObject(null);
        playerReference.GetPlayerInput().BlockInput(false);
        chatPlateControl.AssignChatPlate(playerReference.GetTransform(),chat);
    }

    [PunRPC]
    public void ShutDownChat()
    {
        
        chatPlateControl.UnAssignChatPlate(playerReference.GetTransform());
    }
    
    
}
