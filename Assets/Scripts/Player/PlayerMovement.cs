using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerProperties))]
public class PlayerMovement : MonoBehaviour {

    private PlayerProperties playerProperties = null;
    private KeyCode upControll;
    private KeyCode downControll;

    private void Awake()
    {
        playerProperties = GetComponent<PlayerProperties>();
    }

    private void Update () {
        if (!playerProperties.canMove) { return; }
        SetPlayerControlls();
        MovePlayer();
    }

    private void SetPlayerControlls()
    {
        if(playerProperties.playerType == PlayerType.Player1)
        {
            upControll = PlayersMovementManager.player1Controll["up"];
            downControll = PlayersMovementManager.player1Controll["down"];
        }else if (playerProperties.playerType == PlayerType.Player2)
        {
            upControll = PlayersMovementManager.player2Controll["up"];
            downControll = PlayersMovementManager.player2Controll["down"];
        }
    }

    private void MovePlayer()
    {
        if (Input.GetKey(downControll))
        {
            transform.Translate(Vector3.down * playerProperties.speed * Time.deltaTime);
        }
        else if (Input.GetKey(upControll))
        {
            transform.Translate(Vector3.up * playerProperties.speed * Time.deltaTime);
        }
    }

    private void DisableOnStartGame()
    {
        if(playerProperties.playerType == PlayerType.AI)
        {
            this.enabled = false;
        }
    }
}
