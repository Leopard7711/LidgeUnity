using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerControl : MonoBehaviourPun, IPlayerReference
{ 
    private PlayerReference playerReference;


    public bool GetIsMine()
    {
        return photonView.IsMine;
    }

    public void SetPlayerReference(PlayerReference playerReference)
    {
        this.playerReference = playerReference;
    }
}