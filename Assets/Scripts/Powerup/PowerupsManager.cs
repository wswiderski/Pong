using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsManager : MonoBehaviour {

    public Vector2 border = new Vector2(4.8f, 3.8f);
    public float powerupLifeTime = 3f;

    public GameObject[] powerups;

    private float time = 0f;
    private float nextPowerupTime;
    private PlayManager playManager;

    private void Awake()
    {
        playManager = GetComponent<PlayManager>();
        nextPowerupTime = Random.Range(5f, 8f);
    }

    void Update () {
        if (!playManager.gameInProgres && playManager.ball.canMove) { return; }

        time += Time.deltaTime;
		if(time >= nextPowerupTime)
        {
            GameObject randomPowerup = powerups[Random.Range(0, powerups.Length - 1)];
            Instantiate(randomPowerup, new Vector2(Random.Range(-border.x, border.x), Random.Range(-border.y, border.y)), Quaternion.identity);
            Invoke("Die", powerupLifeTime);
            nextPowerupTime += Random.Range(5f, 8f);
        }
	}

    private void Die()
    {
        GameObject powerup = GameObject.FindGameObjectWithTag("POWERUP");
        if(powerup != null)
        {
            Destroy(powerup);
        }
    }


}
