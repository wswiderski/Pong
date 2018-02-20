using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    public PlayManager playManager;

    public bool canMove = true;
    public float startSpeed = 2.5f;
    public float speed = 2.5f;

    [SerializeField] private float deltaSpeed = 0.1f;
    
	void Start () {
        startSpeed = speed;
        SetRandomRotation();
	}
	
	void Update () {
        if (!canMove) { return; }

        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("BORDERS"))
        {
            float angle = -transform.eulerAngles.z;
            RotateBall(angle);
            if (playManager.gameInProgres)
            {
                IncreaseBallSpeed(deltaSpeed);
                gameObject.GetComponent<AudioSource>().Play();
            }
        }
    }

    public void RotateBall(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
    }

    public void RotateBallWithRandomModification(float angle)
    {
        float randomNumber = Random.Range(-3.5f, 3.5f);
        RotateBall(angle + randomNumber);
    }

    public void IncreaseBallSpeed(float value)
    {
        speed += value;
    }

    public float GetRotation()
    {
        return transform.localEulerAngles.z;
    }

    private float ReturnRandomBallRotationAngle()
    {
        float randomFactor = Random.Range(-60f, 60f);
        float angle = Random.value >= 0.5f ? randomFactor : 180f + randomFactor;
        return angle;
    }

    public void ResetBall()
    {
        transform.position = new Vector2(0f, 0f);
    }

    public void SetRandomRotation()
    {
        float randomZRotation = this.ReturnRandomBallRotationAngle();
        transform.rotation = Quaternion.Euler(0f, 0f, randomZRotation);
    }
}
