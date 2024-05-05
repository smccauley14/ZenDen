using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PosterController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private VideoPlayer video;
    [SerializeField] private string scene;
    [SerializeField] private GameObject objectToHighlight;
    [SerializeField] private Button interactiveButton;
    [SerializeField] private GameObject sensoryRoomManager;
    [SerializeField] private GameObject[] gameControllers;
    private float distanceThreshold = 5f;
    private bool clickable;
    private Color objectToHighlightDefaultColor;
    private bool isVideoPlaying = false;

    // Adjust this angle to set the field of view
    public float fieldOfViewAngle = 60f;

    private void Start()
    {
        objectToHighlightDefaultColor = objectToHighlight.GetComponent<MeshRenderer>().material.color;
    }
    // Check if the player is looking at this object
    private bool IsInSight()
    {
        interactiveButton.gameObject.SetActive(false);
        if (playerCamera == null)
        {
            Debug.LogError("Player camera reference not set.");
            return false;
        }

        var directionToObject = (transform.position - playerCamera.transform.position).normalized;
        var forwardDirection = playerCamera.transform.forward;
        var distanceFromObject = Vector3.Distance(transform.position, playerCamera.transform.position);
        float dotProduct = Vector3.Dot(forwardDirection, directionToObject);
        SetDistanceThreshold();

        if (dotProduct > 0.85f)
        {

            objectToHighlight.GetComponent<MeshRenderer>().material.color = Color.red;
            if (distanceFromObject <= distanceThreshold && !isVideoPlaying)
            {
                objectToHighlight.GetComponent<MeshRenderer>().material.color = Color.green;
                interactiveButton.gameObject.SetActive(true);
                clickable = true;
            }


            return true;
        }

        objectToHighlight.GetComponent<MeshRenderer>().material.color = objectToHighlightDefaultColor;
        clickable = false;
        return false;
    }

    private void SetDistanceThreshold()
    {
        if (gameObject.name == "DoorR")
        {
            distanceThreshold = 8f;
        }
        else
        {
            distanceThreshold = 5f;
        }
    }

    void Update()
    {
        IsInSight();
    }

    public void PosterClick()
    {
        if (clickable)
        {
            if (video != null)
            {
                video.gameObject.SetActive(true);
                video.Play();
                isVideoPlaying = true;
                video.loopPointReached += OnVideoEnd;
                ChangeNextGameObject();
                sensoryRoomManager.GetComponent<SensoryRoomManager>().DisableAllImages();
                interactiveButton.gameObject.SetActive(false);
                foreach (var controller in gameControllers)
                {
                    controller.GetComponent<Image>().enabled = false;
                }
            }

            if (!String.IsNullOrEmpty(scene))
            {
                if (gameObject.name != "DoorR")
                {
                    ChangeNextGameObject();
                }
                SceneManager.LoadSceneAsync(scene);
            }
                
        }
    }

    private static void ChangeNextGameObject()
    {
        PlayerPrefs.SetInt(SettingKeys.SensoryRoomObjectKey, PlayerPrefs.GetInt(SettingKeys.SensoryRoomObjectKey, SettingKeys.SensoryRoomObjectDefaultValue) + 1);
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
        isVideoPlaying = false;
        foreach (var controller in gameControllers)
        {
            controller.GetComponent<Image>().enabled = true;
        }
    }
}