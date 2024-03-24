using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour,IPlayerReference
{
    private PlayerReference playerReference;
    
    private bool blockInput;
    private Dictionary<string,int> inputDict;
    
    private void Start()
    {
        blockInput = false;
        InitDict();
    }

    private void Update()
    {
        
        UpdateDict();
    }

    
    public int GetInputData(string name)
    {
        if (blockInput)
        {
            return 0;
        }
        else
        {
            return inputDict[name];
        }
        
    }

    private void InitDict()
    {
        inputDict = new Dictionary<string, int>();
        inputDict["Hori"] = 0;
        inputDict["Vert"] = 0;
    }

    private void UpdateDict()
    {
        inputDict["Hori"] = (int)Input.GetAxisRaw("Horizontal");
        inputDict["Vert"] = (int)Input.GetAxisRaw("Vertical");
    }

    public void SetPlayerReference(PlayerReference playerReference)
    {
        this.playerReference = playerReference;
    }

    public void BlockInput(bool state)
    {
        
        blockInput = state;
    }

    public bool GetBlockInput()
    {
        return blockInput;
    }
}
