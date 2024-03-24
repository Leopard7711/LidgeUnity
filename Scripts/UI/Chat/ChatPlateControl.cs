using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatPlateControl : MonoBehaviour
{
    private ChatPlate[] chatPlates;

    private void Start()
    {
        chatPlates = GetComponentsInChildren<ChatPlate>();
        
    }

    public void AssignChatPlate(Transform target,String chat)
    {
        ChatPlate transformNullPlate= null;
        ChatPlate transformSamePlate= null;
        print(target+":"+chat);

        foreach (var chatPlate in chatPlates)
        {
            Transform _trans = chatPlate.GetTargetTransform();
            
            if (_trans == target)
            {
                transformSamePlate = chatPlate;
            }
            else if(_trans == null)
            {
                transformNullPlate = chatPlate;
            }

        }

        if (transformSamePlate != null)
        {
            transformSamePlate.Show(target,chat);
            return;
        } 
        if (transformNullPlate != null)
        {
            transformNullPlate.Show(target,chat);
            return;
        }
        print("Failed to Showing Chat");
    }

    public void UnAssignChatPlate(Transform target)
    {
        ChatPlate transformSamePlate= null;

        foreach (var chatPlate in chatPlates)
        {
            Transform _trans = chatPlate.GetTargetTransform();
            
            if (_trans == target)
            {
                transformSamePlate = chatPlate;
            }
            

        }
        if (transformSamePlate != null)
        {
            transformSamePlate.Hide();
            
        } 
    }
    
}
