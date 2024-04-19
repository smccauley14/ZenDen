using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragDropToDistance : MonoBehaviour
{
    //followed this tutorial for basic touchscreen dragging: https://www.youtube.com/watch?v=zo1dkYfIJVg

    //variables
    [SerializeField] private InputAction press;
    [SerializeField] private InputAction screenPosition;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject yellowHighlight;
    [SerializeField] private GameObject redHighlight;
    [SerializeField] private GameObject greenCorrectIcon;
    private ShapeSorting_GameManager gameManagerScript;
    private Vector3 centrePosition = new Vector3(-0.614f, 11.33f, 7.18f);
    private bool noTriggers;

    private Vector3 currentScreenPosition;
    private float targetZ;
    private float cameraDifferential = 5.5f;
    private Rigidbody objectRB;
    private bool isDragging;

    private float moveSpeed = 0.9f;
    [SerializeField] private string targetTag;

    private bool isMoving = false; // Flag to track whether movement is in progress
    private bool isAtMiddle = false;//to check whether object has reached middle;
    private Transform targetTransform; // Reference to the transform of the target object



    private Vector3 worldPosition //returns the position of the clicked on object, relevant to the camera
    {
        get
        {
            float z = playerCamera.WorldToScreenPoint(transform.position).z;
            return playerCamera.ScreenToWorldPoint(currentScreenPosition + new Vector3(0, 0, z));
        }
    }

    private bool isClickedOn //returns true if raycast hits an item with the drag object script attached
    {
        get
        {
            Ray ray = playerCamera.ScreenPointToRay(currentScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) )
            {
                return hit.transform == transform;
            }

            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //getting GameManager
        gameManagerScript = GameObject.Find("GameManager").GetComponent<ShapeSorting_GameManager>();

        //setting the target point in the Z axis for objects to be dragged into
        playerCamera = Camera.main;
        targetZ = playerCamera.transform.position.z + cameraDifferential;

        //getting rigid body component
        objectRB = GetComponent<Rigidbody>();

    }

    private void Awake()
    {
        //enabling interaction events
        screenPosition.Enable();
        press.Enable();

        //to allow you to assess where on the screen has been clicked (I think)
        screenPosition.performed += context => { currentScreenPosition = context.ReadValue<Vector2>(); };

        //declaring what should happen press interaction starts
        press.performed += _ =>
        {

            if (isClickedOn)
            {
                StartCoroutine(Drag());
            }

        };

        //declaring what should happen press interaction ends
        press.canceled += _ =>
        {
            isDragging = false;
            yellowHighlight.SetActive(false);
        };

    }


    //setting noTriggers as true as standard
    //necessary to make the red 'wrong' highlight function properly
    void FixedUpdate()
    {
        noTriggers = true;
    }

    // Update is called once per frame
    void Update()
    {
        //turning off red highlight
        if (noTriggers)
        {
            TurnOffRedHighlight();
        }

        if (isMoving && targetTransform != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (targetTransform.position - transform.position).normalized;

            // Calculate the distance to the target
            float distance = Vector3.Distance(transform.position, targetTransform.position);

            // Move towards the target with interpolation
            transform.position += direction * moveSpeed * Time.deltaTime;

            // Once the distance is very small, stop moving to middle
            if (distance < 0.05f)
            {
                isAtMiddle = true;
                isMoving = false;
            }
        }

        //once middle is reached, move backwards
        if (isAtMiddle)
        {
            MoveBackWard();
        }

    }

    //move object backward and through hole
    private void MoveBackWard()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private IEnumerator Drag()
    {

        //making 'isDragging' script true when object interaction is taking place
        isDragging = true;

        //making highlighter active in scene
        yellowHighlight.SetActive(true);

        //removing any RB physics effects from previous interactions
        objectRB.velocity = new Vector3(0, 0, 0);
        objectRB.angularVelocity = new Vector3(0, 0, 0);

        //finding the necessary offset
        Vector3 offset = transform.position - worldPosition;

        //turning off Rb
        TurnOffRB();


        //pulling object into foreground
        transform.position = new Vector3(0, 0, targetZ);

        //drag object along X axis

        while (isDragging)
        {
            //dragging
            transform.position = worldPosition + offset;

            yield return null;
        }

        //turning RB back on
        TurnOnRB();

    }

    //methods to turn off/on the Rigid Body
    private void TurnOffRB()
    {
        //GetComponent<Rigidbody>().useGravity = false;
        GetComponent<BoxCollider>().enabled = false;
    }
    private void TurnOnRB()
    {
        //droping object - turn RB back on
        //GetComponent<Rigidbody>().useGravity = true;

        //turning rigid body back on
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if object tag matches the target
        if (other.CompareTag(targetTag))
        {
            // Set the target transform and start moving towards it
            greenCorrectIcon.SetActive(true);
            StartCoroutine(SetShapeInactive());
            StartCoroutine(TurnOffGreenCorrectIcon());
            targetTransform = other.transform;
            TurnOffRB();
            isMoving = true;
        }
        //or if object tag doesn't match target
        else if (!other.CompareTag(targetTag))
        {
            redHighlight.SetActive(true);

        }

    }

    //checking whether triggers are empty
    //to turn off 'red highlight' object
    void OnTriggerStay(Collider other)
    {
        noTriggers = false;
    }


    private void TurnOffRedHighlight()
    {
        redHighlight.SetActive(false);
    }

    
    IEnumerator TurnOffGreenCorrectIcon()
    {
        yield return new WaitForSeconds(2f);
        greenCorrectIcon.SetActive(false);
    }

    IEnumerator SetShapeInactive()
    {
        yield return new WaitForSeconds(3f);
        gameManagerScript.shapesInScene--;
        
        //resetting object for next interactions
        isAtMiddle = false;
        TurnOnRB();

        //setting shape inactive
        gameObject.SetActive(false);

    }



}