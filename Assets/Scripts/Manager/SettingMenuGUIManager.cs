using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenuGUIManager : MonoBehaviour {

    public InputField player1Up;
    public InputField player1Down;
    public InputField player2Up;
    public InputField player2Down;
    public Slider musicVolume;
    public Slider effectsVolume;

    private SoundManager soundManager;
    private GUIManager guiManager;

    void Start () {
        soundManager = GetComponent<SoundManager>();
        guiManager = GetComponent<GUIManager>();

        UpdateDisplayOnGUI();   
	}

    public void OnApplyClick()
    {
        SaveChanges();
        OnMainMenuClick();
    }

    public void OnMainMenuClick()
    {
        UpdateDisplayOnGUI();
        guiManager.OnMainMenuClick(false);
    }

    private void SaveChanges()
    {
        PlayersMovementManager.player1Controll["up"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), player1Up.text, true);
        PlayersMovementManager.player1Controll["down"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), player1Down.text, true);
        PlayersMovementManager.player2Controll["up"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), player2Up.text, true);
        PlayersMovementManager.player2Controll["down"] = (KeyCode)System.Enum.Parse(typeof(KeyCode), player2Down.text, true);
        

        soundManager.SetMusicVolume(musicVolume.value);
        soundManager.SetEffectsVolume(effectsVolume.value);
    }

    private void UpdateDisplayOnGUI()
    {
        musicVolume.value = soundManager.GetMusicVolume();
        effectsVolume.value = soundManager.GetEffectVolume();

        player1Up.text = PlayersMovementManager.player1Controll["up"].ToString();
        player1Down.text = PlayersMovementManager.player1Controll["down"].ToString();
        player2Up.text = PlayersMovementManager.player2Controll["up"].ToString();
        player2Down.text = PlayersMovementManager.player2Controll["down"].ToString();
    }
}
