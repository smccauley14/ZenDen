using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using TouchPhase = UnityEngine.TouchPhase;

public class PosterController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float maxDistance = 2f;
    private Vector3 currentScreenPosition;
    private bool isClickable;
    
    // InputAction for object click
    [SerializeField] private InputAction clickAction;
    [SerializeField] private InputAction screenPosition;

    void OnEnable()
    {
        // Enable the click action
        clickAction.Enable();
    }

    void OnDisable()
    {
        // Disable the click action
        clickAction.Disable();
    }

    void Update()
    {
        // Ensure the playerCamera is set
        if (playerCamera == null)
        {
            Debug.LogError("Player camera is not assigned to the ClickableObject script.");
            return;
        }

        // Cast a ray from the player's camera through the center of the screen
        //var ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        //// Check if the ray hits this object
        //if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        //{
        //    if (hit.collider.gameObject == gameObject)
        //    {
        //        isClickable = true;
        //    }
        //    else
        //    {
        //        isClickable = false;
        //    }
        //}
        //else
        //{
        //    isClickable = false;
        //}
    }

    private void Awake()
    {
        // Enabling interaction events
        screenPosition.Enable();
        clickAction.Enable();

        // To allow you to assess where on the screen has been clicked
        screenPosition.performed += context => { currentScreenPosition = context.ReadValue<Vector2>(); };

        // Declaring what should happen press interaction starts
        clickAction.performed += context => PosterClicked();
    }

    void PosterClicked()
    {
        // Cast a ray from the player's camera through the center of the screen
        var ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        // Check if the ray hits any object within maxDistance
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            // Check if the hit point is within the bounds of the poster's collider
            Collider posterCollider = GetComponent<Collider>();

            if (posterCollider != null && posterCollider == hit.collider)
            {
                // Log a message to the debug log
                Debug.Log("Object Clicked: " + gameObject.name);
            }
        }
    }
}
