using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour {

    private const string INFO_TEXT = "To start the game press \"Space\" button" +
        "\n\nDuring game press \"Esc\" to open pause menu";
    private const int SCORE_TO_WIN = 12;

    public PlayerProperties player1;
    public PlayerProperties player2;
    public BallMovement ball;
    public Text startInfoText;
    public Text player1Score;
    public Text player2Score;

    public Text afterWinRestart;
    public Text aftterWinMainMenu;

    public AudioSource gameEndSound;

    private PlayerProperties winner = null;
    private GUIManager guiManager;
    private bool gameStarted = false;
    public bool gameInProgres = false;
    public bool isPauseMenuOpen = false;

    private PowerupsManager powerupsManager;

    public bool GameStarted
    {
        set
        {
            gameStarted = value;
        }
    }

    private void Awake()
    {
        guiManager = GetComponent<GUIManager>();
    }

    void Start () {
        ResetGame();
        StartCoroutine(GameLoop());
	}
    
    public IEnumerator GameLoop()
    {
        yield return StartCoroutine(GameDemoLoop());
        yield return StartCoroutine(PlayMatch());
        winner = GetWinner();

        if (winner != null)
        {
            SetControl(false);
            startInfoText.text = winner.playerType + " is the winner";
            gameEndSound.Play();
            yield return new WaitForSeconds(1.5f);
            Cursor.visible = true;
            startInfoText.enabled = true;
            afterWinRestart.enabled = true;
            aftterWinMainMenu.enabled = true;
        }
    }

    private PlayerProperties GetWinner()
    {
        return player1.Points > player2.Points ? player1 : player2;
    }

    private IEnumerator GameDemoLoop()
    {
        while (!gameStarted)
        {
            yield return new WaitForSeconds(0.3f);
        }
        SetControl(false);
    }

    private IEnumerator PlayMatch()
    {
        startInfoText.enabled = true;
        Cursor.visible = false;
        ResetPlayersPosition();
        ResetBall();
        SetControl(true);
        SetBallControl(false);

        while(!IsWinner())
        {
            if (!gameInProgres && Input.GetKeyDown(KeyCode.Space)) {
                startInfoText.enabled = false;
                gameInProgres = true;
                SetBallControl(true);
            }

            if (gameInProgres && Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPauseMenuOpen)
                {
                    OpenPauseMenu();
                }
                else
                {
                    ReturnToPlay();
                }
            }
            yield return null;
        }
    }

    private bool IsWinner()
    {
        return (player1.Points >= SCORE_TO_WIN || player2.Points >= SCORE_TO_WIN) && Mathf.Abs(player1.Points - player2.Points) >= 2;
    }

    private void OpenPauseMenu()
    {
        Cursor.visible = true;
        SetControl(false);
        isPauseMenuOpen = true;
        guiManager.OnPauseClick();
    }
    
    public void ReturnToPlay()
    {
        Cursor.visible = false;
        SetControl(true);
        isPauseMenuOpen = false;
        guiManager.pauseMenu.enabled = false;
    }

    private void ResetPlayersPosition()
    {
        player1.gameObject.transform.position = player1.startPosition;
        player2.gameObject.transform.position = player2.startPosition;
    }

    private void SetControl(bool canControl)
    {
        player1.canMove = canControl;
        player2.canMove = canControl;
        SetBallControl(canControl);
    }

    private void SetBallControl(bool controll)
    {
        ball.canMove = controll;
    }

    private void ResetPlayers(bool isGameStarted)
    {
        ResetPlayersPosition();
        player1.ResetPoints();
        if (!isGameStarted)
        {
            player1.playerType = PlayerType.AI;
            player2.playerType = PlayerType.AI;
        }
        player2.ResetPoints();
        player1.GetComponent<AIDemo>().enabled = true;
        player2.GetComponent<SimpleAI>().enabled = true;
        player1.speed = player1.startSpeed;
        player1.paddleLenth = player1.startPaddleLenth;
        player1.ResetScale();
        player2.ResetScale();
        player2.speed = player2.startSpeed;
        player2.paddleLenth = player2.startPaddleLenth;

    }

    private void ResetBall()
    {
        ball.transform.position = Vector2.zero;
        ball.speed = ball.startSpeed;
        ball.SetRandomRotation();
    }

    public void ResetAfterGoal()
    {
        ResetBall();
        ResetPlayersPosition();
        SetBallControl(false);
        gameInProgres = false;
    }

    public void ResetGame(bool isGameSerted = false, bool canBallMove = true)
    {
        gameStarted = isGameSerted;
        gameInProgres = false;
        startInfoText.text = INFO_TEXT;
        startInfoText.enabled = !canBallMove ? true : false;
        afterWinRestart.enabled = false;
        aftterWinMainMenu.enabled = false;
        winner = null;
        ResetPlayers(isGameSerted);
        ResetBall();
        SetControl(true);
        SetBallControl(canBallMove);
        powerupsManager.Reset();
        UpdateSocerBoard();
    }

    public void UpdateSocerBoard()
    {
        player1Score.text = player1.Points.ToString();
        player2Score.text = player2.Points.ToString();
    }
}
