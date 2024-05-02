using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PosterController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private VideoPlayer video;
    [SerializeField] private string scene;
    [SerializeField] private GameObject objectToHighlight;
    [SerializeField] private Button interactiveButton;
    private float distanceThreshold = 5f;
    private bool clickable;
    private Color objectToHighlightDefaultColor;

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
            if (distanceFromObject <= distanceThreshold)
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
                video.loopPointReached += OnVideoEnd;
            }

            if(!String.IsNullOrEmpty(scene))
            {
                SceneManager.LoadSceneAsync(scene);
            }
                
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        vp.gameObject.SetActive(false);
    }
}