using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject settingsMenu;
    private const string sensoryRoomScene = "School Sensory Room";

    public void StartGame() => SceneManager.LoadScene(sensoryRoomScene);

    public void LoadSettings() => settingsMenu.SetActive(true);

    public void ExitGame() => Application.Quit();

    public void ExitSettings()
    {
        PlayerPrefs.Save();
        settingsMenu.SetActive(false);
    }
}
