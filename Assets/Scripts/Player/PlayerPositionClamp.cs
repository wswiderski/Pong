using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerProperties))]
public class PlayerPositionClamp : MonoBehaviour {

    private PlayerProperties playerProperties;

    private void Awake()
    {
        playerProperties = GetComponent<PlayerProperties>();
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, playerProperties.playerBorder.y, playerProperties.playerBorder.x),
            transform.position.z);
    }
}
