using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerProperties))]
public class AIDemo : MonoBehaviour
{

    [SerializeField] private Transform ball;
    private PlayerProperties playerProperties = null;

    private void Awake()
    {
        playerProperties = GetComponent<PlayerProperties>();
    }

    void Update()
    {
        DisableOnStartGame();
        if (!playerProperties.canMove) { return; }

        if (ball.transform.position.x <= 0f)
        {
            Vector2 ballPosition = ball.position;
            float distance = Mathf.Abs(ballPosition.y - transform.position.y);
            float factor = playerProperties.speed / distance;
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, ballPosition.y), Time.deltaTime * factor);
        }
    }

    private void DisableOnStartGame()
    {
        if (playerProperties.playerType != PlayerType.AI)
        {
            this.enabled = false;
        }
    }
}
