using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupProperties : MonoBehaviour {

    public PowerupTypes powerupType;
    private float speedIncreaseFactor = 0.2f;
    private float paddleLenthFactor = 0.4f;

    public void ColectPowerup(GameObject player)
    {
        if(player == null)
        {
            Destroy(gameObject);
            return;
        }

        switch (powerupType)
        {
            case (PowerupTypes.SHRINK):
                ShrinkPlayer(player);
                Destroy(gameObject);
                break;
            case (PowerupTypes.ENLARGE):
                EnlargePlayer(player);
                Destroy(gameObject);
                break;
            case (PowerupTypes.SPEED_DOWN):
                SpeedDownPlayer(player);
                Destroy(gameObject);
                break;
            case (PowerupTypes.SPEED_UP):
                SpeedUpPlayer(player);
                Destroy(gameObject);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    private void ShrinkPlayer(GameObject player)
    {
        player.GetComponent<PlayerProperties>().paddleLenth -= paddleLenthFactor;
    }

    private void EnlargePlayer(GameObject player)
    {
        player.GetComponent<PlayerProperties>().paddleLenth += paddleLenthFactor;
    }

    private void SpeedUpPlayer(GameObject player)
    {
        player.GetComponent<PlayerProperties>().speed += speedIncreaseFactor;
    }

    private void SpeedDownPlayer(GameObject player)
    {
        player.GetComponent<PlayerProperties>().speed -= speedIncreaseFactor;
    }
}
