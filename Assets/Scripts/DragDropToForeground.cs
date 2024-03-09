using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropToForeground : MonoBehaviour
{
    //followed this tutorial for basic touchscreen dragging: https://www.youtube.com/watch?v=zo1dkYfIJVg

    //variables
    [SerializeField] InputAction press;
    [SerializeField] InputAction screenPosition;
    [SerializeField] Camera playerCamera;
    private Vector3 currentScreenPosition;
    private float cameraDifferential = 10.5f;
    private float targetZ;
    private GameManager gameManager;
    private Rigidbody objectRB;
    private bool isDragging = false;
    //private bool isBouncing = false;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private string colour;

    [SerializeField] GameObject correctParticle;
    [SerializeField] GameObject wrongParticle;

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
            if (Physics.Raycast(ray, out hit) && gameManager.objectsClickedOn == 0)
            {
                //gameManager.objectsClickedOn++;
                return hit.transform == transform;
            }
            return false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //get game manager script
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //get the colour tag of the object from the tag
        colour = gameObject.tag;

        //getting rigid body component
        objectRB = GetComponent<Rigidbody>();

        //setting the target point in the Z axis for objects to be dragged into
        playerCamera = Camera.main;
        targetZ = playerCamera.transform.position.z + cameraDifferential;

        //getting the orginal instantiated position/rotation
        originalPosition = transform.position;
        originalRotation = transform.rotation;
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


    }


    private IEnumerator Drag()
    {

        //telling the game manager to count how many objects have been clicked on
        //to avoid lifting more than one
        gameManager.objectsClickedOn++;

        //making 'isDragging' script true when object interaction is taking place
        isDragging = true;

        //play 'picked up popping' sound effect
        gameManager.gameAudio.PlayOneShot(gameManager.pickedUpSound);

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

            gameManager.handObject.SetActive(true);
            gameManager.handObject.transform.position = worldPosition + new Vector3(3f, 2.5f, 1f);

            yield return null;
        }

        //reducing variable in GameManager by 1
        gameManager.objectsClickedOn--;

        //turning RB back on
        TurnOnRB();

        //make hand disappear
        StartCoroutine(handDisappear());

    }


    private void TurnOffRB()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<BoxCollider>().enabled = false;
    }

    private void TurnOnRB()
    {
        //droping object - turn RB back on
        GetComponent<Rigidbody>().useGravity = true;

        //turning rigid body back on
        GetComponent<BoxCollider>().enabled = true;
    }


    private IEnumerator handDisappear()
    {

        yield return new WaitForSeconds(1f);
        gameManager.handObject.SetActive(false);
    }

    //return the object to where it was first instantiated
    private IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(1.75f);
        //removing any RB physics effects from previous interactions
        objectRB.velocity = new Vector3(0, 0, 0);
        objectRB.angularVelocity = new Vector3(0, 0, 0);

        transform.position = originalPosition;
        transform.rotation = originalRotation;
        

        //isBouncing = false;
    }

    private IEnumerator DestroyDelay ()
    {
        yield return new WaitForSeconds(1f);
        gameManager.dinosInScene--;
        Destroy(gameObject);
    }

    private IEnumerator TurnOnWrongParticle()
    {
        yield return new WaitForSeconds(1.15f);
        wrongParticle.SetActive(true);
    
        
    }

    private IEnumerator TurnOffWrongParticle()
    {
        yield return new WaitForSeconds(2.5f);
        wrongParticle.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        //if the object is not being dragged (i.e. if it has been dropped)
        if (!isDragging)
        {
            //if the object dropped is the same colour as the bin, destroy object
            if (other.CompareTag(colour))
            {
                StartCoroutine(DestroyDelay());
                //play 'correct' sound effect
                gameManager.gameAudio.PlayOneShot(gameManager.correctSound);
                correctParticle.SetActive(true);
            }
            //if the object is a different colour, bounce object vertically
            else if (!other.CompareTag(colour))
            {
                //other.gameObject.transform.position = gameManager.originalPosition;
                //isBouncing = true;

                //wrongParticle.SetActive(true);
                StartCoroutine(TurnOnWrongParticle());
                StartCoroutine(TurnOffWrongParticle());

                //play 'wrong' sound effect
                gameManager.gameAudio.PlayOneShot(gameManager.wrongSound);

                objectRB.AddForce(new Vector3(0, 1.2f, 0.10f) * 18f, ForceMode.Impulse);

                //return to original position after a moment.

                StartCoroutine(ReturnToOriginalPosition());
            }
            
        }

    }
}