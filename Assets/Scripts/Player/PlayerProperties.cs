using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour {

    public PlayerType playerType;
    public float speed = 5f;
    public float paddleLenth = 0f;
    public float reflectionStrenght = 0.2f;
    public Vector2 playerBorder = new Vector2();
    public bool canMove = true;
    public Vector2 startPosition;

    public float startSpeed;
    public float startPaddleLenth;

    private static GameObject upBorder;
    private static GameObject downBorder;
    private int points = 0;

    public int Points
    {
        get
        {
            return points;
        }
    }

    public void AddPoint()
    {
        points++;
    }

    public void ResetPoints()
    {
        points = 0;
    }

    private void Awake()
    {
        upBorder = GameObject.Find("UpBorder");
        downBorder = GameObject.Find("DownBorder");
        startPosition = transform.position;

        paddleLenth = PlayerProperties.CalculatePaddleLenth(gameObject);
        playerBorder = PlayerProperties.GetPlayerBorder(paddleLenth);

        startPaddleLenth = paddleLenth;
        startSpeed = speed;
    }

    public void ResetPosition()
    {
        transform.position = new Vector2(transform.position.x, 0f);
    }

    public static float CalculatePaddleLenth(GameObject player)
    {
        return 2 * player.GetComponent<BoxCollider2D>().bounds.extents.y;
    }

    public static Vector2 GetPlayerBorder(float playerLenth)
    {
        float upBoardEdgePosition = PlayerProperties.CalculateBoardEdgePostion(upBorder, true);
        float downBoardEdgePosition = PlayerProperties.CalculateBoardEdgePostion(downBorder, false);

        return new Vector2(upBoardEdgePosition - playerLenth / 2, downBoardEdgePosition + playerLenth / 2);
    }

    private static float CalculateBoardEdgePostion(GameObject border, bool isUpper)
    {
        float positionY = border.transform.position.y;
        float colliderLenth = upBorder.GetComponent<BoxCollider2D>().bounds.extents.y;

        float boardEdgePosition = isUpper ? positionY - colliderLenth : positionY + colliderLenth;
        return boardEdgePosition;
    }
}
