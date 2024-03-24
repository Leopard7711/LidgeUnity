using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    private const string playerNamePrefKey = "PlayerName";
    void Start()
    {
        string _defaultName = string.Empty;
        InputField _inputField = GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                _defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = _defaultName;
            }
        }
        PhotonNetwork.NickName = _defaultName;
    }

    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.Log("Player Name is empty");
            return;
        }

        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNamePrefKey,value);
    }
   
    void Update()
    {
        
    }
}
