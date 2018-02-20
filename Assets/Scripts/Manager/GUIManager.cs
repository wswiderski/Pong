using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayManager))]
public class GUIManager : MonoBehaviour {

    public Canvas mainMenu;
    public Canvas settingMenu;
    public Canvas gameModeMenu;
    public Canvas exitMenu;
    public Canvas pauseMenu;

    public AudioSource menuClickSound;

    public PlayerProperties player1;
    public PlayerProperties player2;

    private PlayManager playManager;

    private void Awake()
    {
        playManager = gameObject.GetComponent<PlayManager>();
    }

    void Start () {
        settingMenu.enabled = false;
        gameModeMenu.enabled = false;
        exitMenu.enabled = false;
        pauseMenu.enabled = false;
	}

    public void OnStartClick()
    {
        mainMenu.enabled = false;
        gameModeMenu.enabled = true;
        menuClickSound.Play();
    }

    public void OnSettingClick()
    {
        mainMenu.enabled = false;
        settingMenu.enabled = true;
        menuClickSound.Play();
    }

    public void OnExitClick(Canvas entryPoint)
    {
        entryPoint.enabled = false;
        exitMenu.enabled = true;
        menuClickSound.Play();
    }

    public void OnMainMenuClick(bool startNewGame)
    {
        settingMenu.enabled = false;
        gameModeMenu.enabled = false;
        exitMenu.enabled = false;
        pauseMenu.enabled = false;

        mainMenu.enabled = true;

        if (startNewGame)
        {
            playManager.ResetGame();
            GameObject[] powerups = GameObject.FindGameObjectsWithTag("POWERUP");
            foreach(GameObject powerup in powerups)
            {
                Destroy(powerup);
            }
        }
        menuClickSound.Play();
    }

    public void OnExitConfirmClick()
    {
        Application.Quit();
    }

    public void OnSinglePlayerClick()
    {
        playManager.ResetGame(true, false);
        player1.playerType = PlayerType.Player1;
        player2.playerType = PlayerType.AI;
        gameModeMenu.enabled = false;
        menuClickSound.Play();
    }

    public void OnMultiPlayerClick()
    {
        playManager.ResetGame(true, false);
        player1.playerType = PlayerType.Player1;
        player2.playerType = PlayerType.Player2;
        gameModeMenu.enabled = false;
        menuClickSound.Play();
    }

    public void OnPauseClick()
    {
        pauseMenu.enabled = true;
    }

    public void OnPauseExit(bool leaveFromGUI=false)
    {
        playManager.ReturnToPlay();
        if (leaveFromGUI)
        {
            menuClickSound.Play();
        }
    }

    public void OnRestartClick()
    {
        pauseMenu.enabled = false;
        playManager.ResetGame(true, false);
        StartCoroutine(playManager.GameLoop());
        menuClickSound.Play();
    }
}
