using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour,IPlayerReference
{
    private PlayerReference playerReference;
    
    void Start()
    {
        if(playerReference.GetPhotonView().IsMine)
        {
            CameraManager.instance.SetCameraTarget(playerReference.transform);
        }
        
        
    }


    public void SetPlayerReference(PlayerReference playerReference)
    {
        this.playerReference = playerReference;
    }
}
