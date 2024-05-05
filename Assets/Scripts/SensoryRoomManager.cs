using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensoryRoomManager : MonoBehaviour
{    
    [SerializeField] private Image playBookImage;
    [SerializeField] private Image playFeatherImage;
    [SerializeField] private Image playToysImage;
    [SerializeField] private Image playShapesImage;
    [SerializeField] private AudioSource gameAudio;
    [SerializeField] private AudioClip playHandyFiveFemaleVoice;
    [SerializeField] private AudioClip playFeatherFemaleVoice;
    [SerializeField] private AudioClip playHandyFiveMaleVoice;
    [SerializeField] private AudioClip playFeatherMaleVoice;
    [SerializeField] private AudioClip playToysFemaleVoice;
    [SerializeField] private AudioClip playShapesFemaleVoice;
    [SerializeField] private AudioClip playToysMaleVoice;
    [SerializeField] private AudioClip playShapesMaleVoice;

    private string[] roomObjects = new string[] { "HandyFive", "Feather", "Toys", "Shapes" };
    private int nextGame;
    private string voiceGender;

    // Start is called before the first frame update
    void Start()
    {
        nextGame = PlayerPrefs.GetInt(SettingKeys.SensoryRoomObjectKey, SettingKeys.SensoryRoomObjectDefaultValue);
        if (nextGame > roomObjects.Length -1 )
        {
            nextGame = 0;
            PlayerPrefs.SetInt(SettingKeys.SensoryRoomObjectKey, 0);
        }
        voiceGender = PlayerPrefs.GetString(SettingKeys.VoiceKey, SettingKeys.VoiceDefaultValue);
        
        SelectNextGame();
    }

    public void DisableAllImages()
    {
        playBookImage.gameObject.SetActive(false);
        playFeatherImage.gameObject.SetActive(false);
        playToysImage.gameObject.SetActive(false);
        playShapesImage.gameObject.SetActive(false);
    }

    private void SelectNextGame()
    {
        if (roomObjects[nextGame] == "HandyFive")
        {
            playBookImage.gameObject.SetActive(true);
            PlayGameAudio(playHandyFiveFemaleVoice, playHandyFiveMaleVoice);
        }

        if (roomObjects[nextGame] == "Feather")
        {
            playFeatherImage.gameObject.SetActive(true);
            PlayGameAudio(playFeatherFemaleVoice, playFeatherMaleVoice);
        }

        if (roomObjects[nextGame] == "Toys")
        {
            playToysImage.gameObject.SetActive(true);
            PlayGameAudio(playToysFemaleVoice, playToysMaleVoice);
        }

        if (roomObjects[nextGame] == "Shapes")
        {
            playShapesImage.gameObject.SetActive(true);
            PlayGameAudio(playShapesFemaleVoice, playShapesMaleVoice);
        }
    }

    private void PlayGameAudio(AudioClip female, AudioClip male)
    {
        if (voiceGender == "Female")
            gameAudio.PlayOneShot(female);
        if (voiceGender == "Male")
            gameAudio.PlayOneShot(male);
    }
}
