using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SensoryRoomSceneManager : MonoBehaviour
{
    private const string sensoryRoomScene = "School Sensory Room";
    public void LoadSensoryRoom() => SceneManager.LoadScene(sensoryRoomScene);
}
