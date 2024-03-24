using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReference : MonoBehaviour
{
    public static UIReference instance;

    public ChatPlateControl chatPlateControl;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    
    }
    
    
    
}
