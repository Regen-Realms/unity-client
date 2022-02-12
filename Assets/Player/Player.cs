using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 targetPosition;
    
    public Vector3 desiredTargetPosition;

    public float timeToMove;
    public float moveTimeElapsed;

    public Tilemap groundTileMap;
    public Tilemap topTileMap;
    
    public Animator animator;
    public string currentAnimation;

    private bool IsMoving() => transform.position != targetPosition;

    private void Start()
    {
        // animator.speed = timeToMove + 1;
    }

    private void ChangeAnimationState(string animation)
    {
        if (animation.Equals(currentAnimation)) return;

        currentAnimation = animation;
        animator.Play(animation);
    }

    private bool CheckForObject(Vector3 position)
    {
        // check collisions on next tile 
        var topTile = topTileMap.WorldToCell(position);
        return topTileMap.GetTile(topTile) != null;
    }

    private void MovePlayer(Vector3 direction)
    {
        if (IsMoving()) return;
     
        origin = transform.position;
        desiredTargetPosition = origin + direction;
        if (CheckForObject(desiredTargetPosition))
        {
            if (direction == Vector3.up)
            {
                ChangeAnimationState("Idle_Up");
            }
            else if (direction == Vector3.down)
            {
                ChangeAnimationState("Idle_Down");
            }
            else if (direction == Vector3.left)
            {
                ChangeAnimationState("Idle_Left");
            }
            else if (direction == Vector3.right)
            {
                ChangeAnimationState("Idle_Right");
            }
            return;
        }
        
        targetPosition = origin + direction;
        
        if (direction == Vector3.up)
        {
            ChangeAnimationState("Walk_Up");
        }
        else if (direction == Vector3.down)
        {
            ChangeAnimationState("Walk_Down");
        }
        else if (direction == Vector3.left)
        {
            ChangeAnimationState("Walk_Left");
        }
        else if (direction == Vector3.right)
        {
            ChangeAnimationState("Walk_Right");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = CheckForObject(desiredTargetPosition) ? Color.red : Color.blue;
        Gizmos.DrawSphere(desiredTargetPosition - new Vector3(0, 0,0), 0.2f);
    }

    private void UpdatePosition()
    {
        if (!IsMoving()) return;

        moveTimeElapsed += Time.deltaTime;
        if (moveTimeElapsed >= timeToMove)
        {
            moveTimeElapsed = 0f;
            transform.position = targetPosition;
        }
        else
        {
            transform.position = Vector3.Lerp(origin, targetPosition, moveTimeElapsed / timeToMove);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var tilePos = groundTileMap.WorldToCell(Vector3Int.FloorToInt(transform.position));
        var tile = groundTileMap.GetTile(tilePos);
        if (tile != null)
        {
            //   TileBase tile = tileMap.GetTile(new Vector3Int(z));
            Debug.Log($"{tile.name} is at {tilePos.x},{tilePos.y}");
        }

        UpdatePosition();

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            MovePlayer(Vector3.right);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            MovePlayer( Vector3.left);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            MovePlayer(Vector3.up);
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            MovePlayer(Vector3.down);
        }

        // Are we stood still?
        if (!IsMoving())
        {
            var nextAnimation = currentAnimation switch
            {
                "Walk_Right" => "Idle_Right",
                "Walk_Left" => "Idle_Left",
                "Walk_Up" => "Idle_Up",
                "Walk_Down" => "Idle_Down",
                _ => currentAnimation
            };
        
            ChangeAnimationState(nextAnimation);
        }
    }
}