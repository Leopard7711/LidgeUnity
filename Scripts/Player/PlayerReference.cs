using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
public interface IPlayerReference
{
    public void SetPlayerReference(PlayerReference playerReference);
}


public class PlayerReference : MonoBehaviour
{
    private Transform trans;
    private PhotonView photonView;

    private PlayerControl playerControl;
    private PlayerMove playerMove;
    private PlayerInput playerInput;
    private PlayerAnimation playerAnimation;
    private PlayerChat playerChat;
    private PlayerCamera playerCamera;
    private void Awake()
    {
        trans = transform;
        
        playerControl = GetComponent<PlayerControl>();
        playerControl.SetPlayerReference(this);
        playerMove = GetComponent<PlayerMove>();
        playerMove.SetPlayerReference(this);
        playerInput = GetComponent<PlayerInput>();
        playerInput.SetPlayerReference(this);
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAnimation.SetPlayerReference(this);
        playerChat = GetComponent<PlayerChat>();
        playerChat.SetPlayerReference(this);
        playerCamera = GetComponent<PlayerCamera>();
        playerCamera.SetPlayerReference(this);

        photonView = GetComponent<PhotonView>();

    }

   
    public PhotonView GetPhotonView()
    {
        return photonView;
    }

    public Transform GetTransform()
    {
        return trans;
    }
    
    public PlayerControl GetPlayerControl()
    {
        return playerControl;
    }
    public PlayerMove GetPlayerMove()
    {
        return playerMove;
    }
    public PlayerInput GetPlayerInput()
    {
        return playerInput;
    }
    public PlayerAnimation GetPlayerAnimation()
    {
        return playerAnimation;
    }
    
    
}
