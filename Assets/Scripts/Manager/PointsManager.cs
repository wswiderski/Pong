using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsManager : MonoBehaviour {

    [SerializeField] private PlayerProperties player1;
    [SerializeField] private PlayerProperties player2;

    [SerializeField] private PlayManager playManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GOAL"))
        {
            if(collision.gameObject.name == "GoalPlayer1")
            {
                player2.AddPoint();
            }
            else
            {
                player1.AddPoint();
            }
            StartCoroutine(WaitAndContinueGame());
        }
    }

    private IEnumerator WaitAndContinueGame()
    {
        yield return new WaitForSeconds(0.1f);
        playManager.UpdateSocerBoard();
        playManager.ResetAfterGoal();
    }
}
