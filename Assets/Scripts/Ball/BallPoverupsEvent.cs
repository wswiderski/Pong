using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPoverupsEvent : MonoBehaviour {

    public GameObject lastTouchedBy = null;
    private PowerupProperties powerupPrperties = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("POWERUP"))
        {
            powerupPrperties = collision.gameObject.GetComponent<PowerupProperties>();
            powerupPrperties.ColectPowerup(lastTouchedBy);
        }
    }
}
