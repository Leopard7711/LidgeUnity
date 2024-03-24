using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
public enum CharStates
{
    PlayerEast = 1,
    PlayerSouth =2,
    PlayerWest = 3,
    PlayerNorth = 4,
    IdleEast = 5,
    IdleSouth = 6,
    IdleWest = 7,
    IdleNorth = 8
}
public class PlayerAnimation : MonoBehaviour,IPlayerReference
{
    private PlayerReference playerReference;
    private bool isMine;
    
    public int lookingFor = 1;
    string animationState = "AnimationState";
    private Vector2 playerDir;
    Animator animator;
    private SpriteRenderer renderer;
    private PlayerMove playerMove;
    

    private void Start()
    {
        isMine = playerReference.GetPlayerControl().GetIsMine();
        
        animator = GetComponent<Animator>();
        lookingFor = (int)CharStates.PlayerSouth;
        animator.SetInteger(animationState, (int)CharStates.PlayerSouth);
        playerDir = new Vector2(0,-1);
        renderer = GetComponent<SpriteRenderer>();

        playerMove = playerReference.GetPlayerMove();

        IdleState();
    }

    public void LookAt(CharStates look)
    {
        lookingFor = (int)look;
        
    }

    public int GetLookingFor()
    {
        return lookingFor;
    }

    
    
    private void Update()
    {
        if (!isMine)
        {
            return;
        }
        
        UpdateAnimationState(playerMove.GetDir());
    }
    public void SetPlayerReference(PlayerReference playerReference)
    {
        this.playerReference = playerReference;
    }
    void UpdateAnimationState(Vector2 dir)
    {
        if (dir.x > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.PlayerEast);
            playerDir = new Vector2(1,0);
            lookingFor = (int)CharStates.PlayerEast;
            
        }
        else if (dir.x < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.PlayerWest);  
            playerDir = new Vector2(-1,0);
            lookingFor = (int)CharStates.PlayerWest;
        }
        else if (dir.y > 0)
        {
            animator.SetInteger(animationState, (int)CharStates.PlayerNorth);
            playerDir = new Vector2(0,1);
            lookingFor = (int)CharStates.PlayerNorth;
        }
        else if (dir.y < 0)
        {
            animator.SetInteger(animationState, (int)CharStates.PlayerSouth);
            playerDir = new Vector2(0,-1);
            lookingFor = (int)CharStates.PlayerSouth;
        }
        else
        {
            IdleState();
        }
    }
    
    void IdleState()
    {
        switch (lookingFor)
        {
            case 1:
                animator.SetInteger(animationState, (int)CharStates.IdleEast);
                
                playerDir = new Vector2(1,0);
                break;
            case 2:
                animator.SetInteger(animationState, (int)CharStates.IdleSouth);
                playerDir = new Vector2(0,-1);
                break;
            case 3:
                animator.SetInteger(animationState, (int)CharStates.IdleWest);
                playerDir = new Vector2(-1,0);
                break;
            case 4:
                animator.SetInteger(animationState, (int)CharStates.IdleNorth);
                playerDir = new Vector2(0,1);
                break;

        }
    }

    public Vector2 GetPlayerDir()
    {
        return playerDir;
    }
    public void LightenSprite()
    {
        
        if (lookingFor == (int)CharStates.PlayerNorth)
        {
            
            Color _color = renderer.color;
            _color.a = 140 / 255f;
            renderer.color = _color;
            
        }
    }
    public void NormalizeSprite()
    {
        
        Color _color = renderer.color;
        _color.a = 1;
        renderer.color = _color;
    }

   
}
