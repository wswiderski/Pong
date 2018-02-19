using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource backgroundMusic;
    public AudioSource collisionMusic;
    public AudioSource clickMusic;
    public AudioSource winMusic;

    private float defaultEffectVolume = 0.5f;
    private float defaultMusicVolume = 0.1f;

    private void Awake()
    {
        collisionMusic.volume = clickMusic.volume = winMusic.volume = defaultEffectVolume;
        backgroundMusic.volume = defaultMusicVolume;
    }

    public float GetMusicVolume()
    {
        return backgroundMusic.volume;
    }

    public float GetEffectVolume()
    {
        return collisionMusic.volume;
    }

    public void SetMusicVolume(float value)
    {
        backgroundMusic.volume = value;
    }

    public void SetEffectsVolume(float value)
    {
        collisionMusic.volume = clickMusic.volume = winMusic.volume = value;
    }
}
