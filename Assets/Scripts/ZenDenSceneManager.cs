using UnityEngine;
using UnityEngine.SceneManagement;

public class ZenDenSceneManager : MonoBehaviour
{
    private const string sensoryRoomScene = "School Sensory Room";
    public void LoadSeneoryRoom() => SceneManager.LoadScene(sensoryRoomScene);
}
