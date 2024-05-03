using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip guitarMusic;
    [SerializeField] private AudioClip synthMusic;
    [SerializeField] private AudioClip noiseMusic;

    private AudioSource gameAudio;
    private string backgroundMusic;
    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        backgroundMusic = PlayerPrefs.GetString(SettingKeys.BackgroundMusicKey, SettingKeys.BackgroundMusicDefaultValue);
        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        if (backgroundMusic == "Guitar")
            gameAudio.clip = guitarMusic;

        if (backgroundMusic == "Noise")
            gameAudio.clip = noiseMusic;

        if (backgroundMusic == "Synth")
            gameAudio.clip = synthMusic;

        gameAudio.loop = true;
        gameAudio.Play();

    }
}
