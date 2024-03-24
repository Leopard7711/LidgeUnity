using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatPlate : MonoBehaviour
{
    private static int wrapLimit =9;
    private static WaitForSeconds hideChatPlateWFS = new WaitForSeconds(7f);
    private Image image;
    private Text text;
    private bool activated;
    private Transform targetTransform;
    private RectTransform trans;

    private Coroutine previousCoroutine;

    private void Start()
    {
        trans = GetComponent<RectTransform>();
        
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();

        image.enabled = false;
        text.enabled = false;
    }

    private void Update()
    {
        
        if (activated)
        {
            if (targetTransform == null)
            {
                return;
            }
            trans.position = Camera.main.WorldToScreenPoint(targetTransform.position+Vector3.up*2.2f);
        }
        
    }

    public void Show(Transform trans,string chat)
    {
        if (previousCoroutine != null)
        {
            StopCoroutine(previousCoroutine);
        }
        
        activated = true;
        image.enabled = true;
        text.enabled = true;
        text.text = WrapText(chat);
        targetTransform = trans;
        
        previousCoroutine =StartCoroutine("CoHideAfterDelay");
    }

    public void Hide()
    {
        activated = false;
        image.enabled = false;
        text.enabled = false;
        text.text = "";
        targetTransform = null;
    }
    string WrapText(string input)
    {
        string result = "";
        int count = 0;

        foreach (char c in input.TrimEnd())
        {
            result += c;
            count++;

            if (count >= wrapLimit)
            {
                result += "\n";
                count = 0;
            }
           
        }
        if (result.EndsWith("\n"))
        {
            result = result.Remove(result.Length - 1);
        }
        return result;
    }

    IEnumerator CoHideAfterDelay()
    {
        
        yield return hideChatPlateWFS;
        Hide();
    }

    public Transform GetTargetTransform()
    {
        return targetTransform;
    }

    public bool GetActivated()
    {
        return activated;
    }
}
