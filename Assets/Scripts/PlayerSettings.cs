using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] Button guitarButton;
    [SerializeField] Button noiseButton;
    [SerializeField] Button synthButton;
    [SerializeField] Button levelOneButton;
    [SerializeField] Button levelTwoButton;
    [SerializeField] Button levelThreeButton;
    [SerializeField] Button maleVoiceButton;
    [SerializeField] Button femaleVoiceButton;

    private string backgroundVoice;
    private int level;
    private string voice;

    private void Start()
    {
        GetCurrentSettings();
        HighlightActiveLevel();
    }

    private void HighlightActiveLevel()
    {
        RemoveOptionsOutlines();
        SetMusicOutline();
        SetLevelOutline();
        SetMucisOutline();

    }
    private void SetMusicOutline() 
    {
        if (backgroundVoice == "Guitar")
            EnableButtonOutline(guitarButton);

        if (backgroundVoice == "Noise")
            EnableButtonOutline(noiseButton);

        if (backgroundVoice == "Synth")
            EnableButtonOutline(synthButton);
    }

    private void SetLevelOutline()
    {
        if (level == 1)
            EnableButtonOutline(levelOneButton);

        if (level == 2)
            EnableButtonOutline(levelTwoButton);

        if (level == 3)
            EnableButtonOutline(levelThreeButton);
    }

    private void SetMucisOutline()
    {
        if (voice == "Male")
            EnableButtonOutline(maleVoiceButton);

        if (voice == "Female")
            EnableButtonOutline(femaleVoiceButton);
    }

    private void EnableButtonOutline(Button button)
    {
        button.GetComponent<Outline>().enabled = true;
    }

    private void RemoveOptionsOutlines()
    {
        guitarButton.GetComponent<Outline>().enabled = false;
        noiseButton.GetComponent<Outline>().enabled = false;
        synthButton.GetComponent<Outline>().enabled = false;
        NewMethod();
        DisableVoiceButtons();
    }

    private void NewMethod()
    {
        levelOneButton.GetComponent<Outline>().enabled = false;
        levelTwoButton.GetComponent<Outline>().enabled = false;
        levelThreeButton.GetComponent<Outline>().enabled = false;
    }

    private void DisableVoiceButtons()
    {
        maleVoiceButton.GetComponent<Outline>().enabled = false;
        femaleVoiceButton.GetComponent<Outline>().enabled = false;
    }

    private void GetCurrentSettings()
    {
        backgroundVoice = PlayerPrefs.GetString(SettingKeys.BackgroundMusicKey, SettingKeys.BackgroundMusicDefaultValue);
        level = PlayerPrefs.GetInt(SettingKeys.LevelKey, SettingKeys.LevelDefaultValue);
        voice = PlayerPrefs.GetString(SettingKeys.VoiceKey, SettingKeys.VoiceDefaultValue);
    }

    public void SaveMusic(string music)
    {
        this.backgroundVoice = music;
        PlayerPrefs.SetString(SettingKeys.BackgroundMusicKey, music);
        PlayerPrefs.Save();
        HighlightActiveLevel();
    }
    public void SaveVoice(string voice)
    {
        this.voice = voice;
        PlayerPrefs.SetString(SettingKeys.VoiceKey, voice);
        PlayerPrefs.Save();
        HighlightActiveLevel();
    }
    public void SaveLevel(int level)
    {
        this.level = level;
        PlayerPrefs.SetInt(SettingKeys.VoiceKey, level);
        PlayerPrefs.Save();
        HighlightActiveLevel();
    }


}
