using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersMovementManager : MonoBehaviour {

    public static Dictionary<string, KeyCode> player1Controll = new Dictionary<string, KeyCode>() {
        { "up", KeyCode.W},
        { "down", KeyCode.S}
    };

    public static Dictionary<string, KeyCode> player2Controll = new Dictionary<string, KeyCode>() {
        { "up", KeyCode.P},
        { "down", KeyCode.L}
    };
}
