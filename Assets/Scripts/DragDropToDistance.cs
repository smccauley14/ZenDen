using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropToDistance : MonoBehaviour
{
    //followed this tutorial for basic touchscreen dragging: https://www.youtube.com/watch?v=zo1dkYfIJVg

    //variables
    [SerializeField] InputAction press;
    [SerializeField] InputAction screenPosition;
    [SerializeField] Camera playerCamera;
    private Vector3 currentScreenPosition;
    private float targetZ;
    private float cameraDifferential = 20f;
    private Rigidbody objectRB;
    private bool isDragging;

    private float moveSpeed = 2.5f;
    public string targetTag;

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


        //setting the target point in the Z axis for objects to be dragged into
        playerCamera = Camera.main;
        targetZ = playerCamera.transform.position.z + cameraDifferential;

        //getting rigid body component
        objectRB = GetComponent<Rigidbody>();

        //getting the orginal instantiated position/rotation
        //originalPosition = transform.position;
        //originalRotation = transform.rotation;

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
        };

    }

    // Update is called once per frame
    void Update()
    {

        if (isMoving && targetTransform != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (targetTransform.position - transform.position).normalized;

            // Calculate the distance to the target
            float distance = Vector3.Distance(transform.position, targetTransform.position);

            // Move towards the target with interpolation
            transform.position += direction * moveSpeed * Time.deltaTime;

            
            // Once the distance is very small, stop moving to middle
            if (distance < 0.1f)
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

    private void MoveBackWard()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private IEnumerator Drag()
    {

        //making 'isDragging' script true when object interaction is taking place
        isDragging = true;

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
        if (other.CompareTag(targetTag))
        {
            // Set the target transform and start moving towards it
            targetTransform = other.transform;
            TurnOffRB();
            isMoving = true;
        }
    }

    /*
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Circle"))
        {

            // Get the position of the other object
            Vector3 targetPosition = other.transform.position;

            transform.Translate(targetPosition * speed * Time.deltaTime);
            // Move this object to the same position
            //transform.position = targetPosition;

            

            TurnOffRB();
            float targetPositionX = other.transform.position.x;
            float targetPositionY = other.transform.position.y;
            transform.Translate(other.transform.position * speed * Time.deltaTime);

            //Destroy(gameObject);
            
        }

    
    }
    */
}