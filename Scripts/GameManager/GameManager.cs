using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Chat;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using Photon.Realtime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviourPunCallbacks,IManagerReference
{
    private ManagerReference managerReference;
    [SerializeField]private GameObject[] playerPrefab;

  
    [SerializeField]private Text debugText;
    private WaitForSeconds spawnDelayWFS;

    public static Action actionBeforeLeavesRoom;
    
    public override void OnLeftRoom()
    {
        print("룸 나감");
        SceneManager.LoadScene(0);
        
    }
    
    private void Start()
    {
        spawnDelayWFS = new WaitForSeconds(0.1f);
        
        
        if (playerPrefab.Length ==0 || playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
        }
        else
        {
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", Application.loadedLevelName);
            
            StartCoroutine(SpawnPlayer());
        } 
        
    }

    

    IEnumerator SpawnPlayer()
    {
        yield return spawnDelayWFS;
        int _playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        PlayerReference _localPlayerReference = PhotonNetwork.Instantiate(playerPrefab[_playerCount - 1].name,
            Vector3.zero, Quaternion.identity).GetComponent<PlayerReference>();
        managerReference.SetLocalPlayerReference(_localPlayerReference);
        
        print("로컬 플레이어 : "+_localPlayerReference);
    }


    public void LeaveRoom()
    {
       
        actionBeforeLeavesRoom?.Invoke();print(gameObject.activeInHierarchy+":::::");
        StartCoroutine(nameof(CoLeaveRoom));
        
        
    }

    
    IEnumerator CoLeaveRoom()
    {
        
        yield return new WaitForSeconds(0.1f);
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
        
    }
    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
        
        
    }

   
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects
        
    }

    public void SetManagerReference(ManagerReference managerReference)
    {
        this.managerReference = managerReference;
    }

    public void SetLocalPlayerReference(PlayerReference playerReference)
    {
        // 여긴 쓸일 없음
    }
}
