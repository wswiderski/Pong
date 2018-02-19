using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerProperties))]
public class PlayerReflection : MonoBehaviour {

    public PlayManager playManager;

    private enum PaddleZone
    {
        UP45, UP30, UP15, ZERO, DOWN45, DOWN30, DOWN15
    }

    private Dictionary<PaddleZone, float> reflectionAngle = new Dictionary<PaddleZone, float>()
    {
        {PaddleZone.UP45, 45f },
        {PaddleZone.UP30, 30f },
        {PaddleZone.UP15, 15f },
        {PaddleZone.ZERO, 0f },
        {PaddleZone.DOWN15,345f  },
        {PaddleZone.DOWN30, 330f },
        {PaddleZone.DOWN45, 315f }
    };

    private PlayerProperties playerProperties;

    private void Awake()
    {
        playerProperties = gameObject.GetComponent<PlayerProperties>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BALL"))
        {
            GameObject ball = collision.gameObject;
            if (playManager.gameInProgres)
            {
                ball.GetComponent<AudioSource>().Play();
            }
            float angle = GetReflectionAngle(transform.position, ball.transform.position);
            RotateBallAfterReflection(ball, angle);
        }
    }

    private float GetReflectionAngle(Vector2 paddlePosition, Vector2 ballPosition)
    {
        PaddleZone zone = GetPaddleZone(paddlePosition, ballPosition);
        float angle = reflectionAngle[zone];
        return transform.position.x <= 0f ? angle : 180 - angle;
    }

    private void RotateBallAfterReflection(GameObject ball, float angle)
    {
        BallPoverupsEvent ballPoverupsEvent = ball.GetComponent<BallPoverupsEvent>();
        BallMovement ballMovement = ball.GetComponent<BallMovement>();

        ballPoverupsEvent.lastTouchedBy = gameObject;
        ballMovement.RotateBallWithRandomModification(angle);
        if (playManager.gameInProgres)
        {
            ballMovement.IncreaseBallSpeed(playerProperties.reflectionStrenght);
        }
    }

    //TODO: refactor funkcji bo brzydka
    private PaddleZone GetPaddleZone(Vector2 paddlePosition, Vector2 ballPosition)
    {
        float paddleLenth = playerProperties.paddleLenth;
        float distance = ballPosition.y - paddlePosition.y;
        float distanceFactor = Mathf.Abs(distance);
        float zoneLenth = paddleLenth / 7;

        if (distanceFactor >= 2 * zoneLenth + 1 / 2 * zoneLenth)
        {
            if (distance > 0f)
            {
                return PaddleZone.UP45;
            }
            else
            {
                return PaddleZone.DOWN45;
            }
        }else if(distanceFactor >= zoneLenth + 1 / 2 * zoneLenth)
        {
            if (distance > 0f)
            {
                return PaddleZone.UP30;
            }
            else
            {
                return PaddleZone.DOWN30;
            }
        }else if (distanceFactor >= 1 / 2 * zoneLenth)
        {
            if (distance > 0f)
            {
                return PaddleZone.UP15;
            }
            else
            {
                return PaddleZone.DOWN15;
            }
        }
        else
        {
            return PaddleZone.ZERO;
        }
    }
}
