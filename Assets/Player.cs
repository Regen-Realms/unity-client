using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Vector3 origin;
    public Vector3 destination;

    public bool isMoving;
    public float timeToMove;
    public float moveTimeElapsed;

    public Tilemap tileMap;

    // Update is called once per frame
    void Update()
    {
     
        var tilePos = tileMap.WorldToCell(Vector3Int.FloorToInt(transform.position));
        var tile = tileMap.GetTile(tilePos);
        if (tile != null)
        {
            //   TileBase tile = tileMap.GetTile(new Vector3Int(z));
            Debug.Log($"{tile.name} is at {tilePos.x},{tilePos.y}");
        }

        if (!isMoving)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                origin = transform.position;
                destination = transform.position + Vector3.right;
                isMoving = true;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                origin = transform.position;
                destination = transform.position + Vector3.left;
                isMoving = true;
            }
            else if (Input.GetAxisRaw("Vertical") > 0)
            {
                origin = transform.position;
                destination = transform.position + Vector3.up;
                isMoving = true;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                origin = transform.position;
                destination = transform.position + Vector3.down;
                isMoving = true;
            }
        }
        else
        {
            moveTimeElapsed += Time.deltaTime;
            if (moveTimeElapsed >= timeToMove)
            {
                isMoving = false;
                moveTimeElapsed = 0f;
                transform.position = destination;
            }
            else
            {
                transform.position = Vector3.Lerp(origin, destination, moveTimeElapsed / timeToMove);
            }
        }
    }
}