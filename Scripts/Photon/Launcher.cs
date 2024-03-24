using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";
    private bool isConnecting;
    [SerializeField] private byte maxPlayersPerRoom = 4;

    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject progressLabel;

    [SerializeField] private Text userNameText;

    public GameObject[] playerPrefab;
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    

    private void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
    }

    public void Connect()
    {
        
        isConnecting = true;
        progressLabel.SetActive(true);
        controlPanel.SetActive(false);
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.NickName = userNameText.text;
        }
        

    }

    public override void OnConnectedToMaster()
    {
        
        Debug.Log("OnConnectedToMaster() was called by PUN");
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("OnDisconnected() was called by PUN with reason {0}", cause);
        if(progressLabel!=null)progressLabel.SetActive(false);
        if(controlPanel !=null)controlPanel.SetActive(true);
        
        
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        
        Debug.Log("OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        
        PhotonNetwork.CreateRoom(null, new RoomOptions{MaxPlayers = maxPlayersPerRoom});
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room.");
        PhotonNetwork.LoadLevel("Room");
        

    }
    
    
}