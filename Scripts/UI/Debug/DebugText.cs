using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    private static Text text;
    private void Awake()
    {
        text= GetComponent<Text>();
    }

    public static void SetText(string s)
    {
        text.text = s;
    }
}
