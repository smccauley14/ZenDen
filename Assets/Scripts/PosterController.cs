using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PosterController : MonoBehaviour
{
    // Reference to the player's camera
    [SerializeField] private Camera playerCamera;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private VideoPlayer video;
    [SerializeField] private String scene;
    private float distanceThreshold = 2.5f;
    private bool clickable;

    // Adjust this angle to set the field of view
    public float fieldOfViewAngle = 60f;

    // Check if the player is looking at this object
    private bool IsInSight()
    {
        if (playerCamera == null)
        {
            Debug.LogError("Player camera reference not set.");
            return false;
        }

        if (Vector3.Distance(transform.position, playerCamera.transform.position) <= distanceThreshold)
        {
            Vector3 directionToObject = (transform.position - playerCamera.transform.position).normalized;
            Vector3 forwardDirection = playerCamera.transform.forward;
            float dotProduct = Vector3.Dot(forwardDirection, directionToObject);

            if (dotProduct > 0.85f)
            {
                particle.Play();
                clickable = true;
                return true;
            }
        }
        particle.Stop();
        clickable = false;
        return false;
    }

    void Update()
    {
        IsInSight();
    }

    public void PosterClick()
    {
        Debug.Log("Poster Clicked");
        if (clickable)
        {
            if (video != null)
            {
                video.gameObject.SetActive(true);
                video.Play();
                video.loopPointReached += OnVideoEnd;
            }

            if(scene != null)
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