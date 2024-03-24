using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerMove : MonoBehaviourPunCallbacks, IPlayerReference
{
    private PlayerReference playerReference;
    private PlayerInput playerInput;
    private bool isMine;
    
    private float x, y;
    private Vector2 dir;
    public Rigidbody2D rigidbody2D;
    public float speed;
    private Transform trans;

    [SerializeField]private Vector2 initPosition;
    
    
    private void Awake()
    {
        trans = transform;
    }
    private void Start()
    {
        playerInput = playerReference.GetPlayerInput();
        rigidbody2D = GetComponent<Rigidbody2D>();
        isMine = playerReference.GetPlayerControl().GetIsMine();

    }
    private void Update()
    {
        if (!isMine)
        {
            return;
        }


        x = playerInput.GetInputData("Hori");
        y = playerInput.GetInputData("Vert");
        dir = new Vector3(x, y, 0).normalized;
        MoveCharacter(dir);
    }

    public void SetPlayerReference(PlayerReference playerReference)
    {
        this.playerReference = playerReference;
    }

    public Vector2 Init()
    {
        return initPosition;
    }

    public void MoveCharacter(Vector2 dir)
    {
        
        rigidbody2D.velocity = dir * speed;
    }

    public void MoveDirectly(Vector2 pos)
    {
        rigidbody2D.position = pos;
    }

    public Vector2 GetDir()
    {
        return dir;
    }
    
    public Vector2 GetPosition()
    {
        return trans.position;
    }

}
