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
    private ColourSorting_GameManager gameManagerScript;
    private ColourSorting_AudioManager audioManagerScript;
    private ColourSorting_ColourSelector colourSelectorScript;
    private Rigidbody objectRB;
    private bool isDragging = false;
    private string colour;
    //private string TrayPositionOfPickedUpObject;

    //particle effects
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

            if (Physics.Raycast(ray, out hit) && gameManagerScript.objectsClickedOn == 0)
            {
                //getting the relevant tray position in order to determine colour
                gameManagerScript.TrayPositionOfPickedUpObject = hit.transform.tag;

                return hit.transform == transform;
            }
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //get game manager script
        gameManagerScript = GameObject.Find("GameManager").GetComponent<ColourSorting_GameManager>();
        audioManagerScript = GameObject.Find("GameManager").GetComponent<ColourSorting_AudioManager>();
        colourSelectorScript = GameObject.Find("GameManager").GetComponent<ColourSorting_ColourSelector>();

        //get the colour tag of the object from the tag
        colour = gameObject.tag;

        //getting rigid body component
        objectRB = GetComponent<Rigidbody>();

        //setting the target point in the Z axis for objects to be dragged into
        playerCamera = Camera.main;
        targetZ = playerCamera.transform.position.z + cameraDifferential;

        //getting the orginal instantiated position/rotation - KEEP IN CODE FOR NOW
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

            if (isClickedOn && !gameManagerScript.UIisActive)//only starts if UI menu is not active
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
        gameManagerScript.objectsClickedOn++;

        //making 'isDragging' script true when object interaction is taking place
        isDragging = true;

        //play 'picked up popping' sound effect
        gameManagerScript.gameAudio.PlayOneShot(gameManagerScript.pickedUpSound);

        //play appropriate colour naming sound;
        audioManagerScript.NamePickedUpColour_MaleVoice(GetColourNumber());

        //removing any RB physics effects from previous interactions
        objectRB.velocity = new Vector3(0, 0, 0);
        objectRB.angularVelocity = new Vector3(0, 0, 0);

        //finding the necessary offset
        Vector3 offset = transform.position - worldPosition;

        //turning off Rb
        TurnOffRB();

        //pulling object into foreground
        transform.position = new Vector3(0, 0, targetZ);

        //drag object
        while (isDragging)
        {
            //dragging
            transform.position = worldPosition + offset;

            gameManagerScript.handObject.SetActive(true);
            gameManagerScript.handObject.transform.position = worldPosition + new Vector3(3f, 2.5f, 1f);

            yield return null;
        }

        //reducing variable in GameManager by 1
        gameManagerScript.objectsClickedOn--;

        //turning RB back on
        TurnOnRB();

        //make hand disappear
        StartCoroutine(handDisappear());
    }

    //methods to turn off/on the Rigid Body
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

    //make the hand disappear after a delay
    private IEnumerator handDisappear()
    {

        yield return new WaitForSeconds(1f);
        gameManagerScript.handObject.SetActive(false);
    }

    //move object to a new random position within bounds
    private IEnumerator ReturnToRandomPosition()
    {
        yield return new WaitForSeconds(1.75f);
        //removing any RB physics effects from previous interactions
        objectRB.velocity = new Vector3(0, 0, 0);
        objectRB.angularVelocity = new Vector3(0, 0, 0);
        transform.position = gameManagerScript.GenerateSpawnPos();
    }

    //return the object to where it was first instantiated - KEEP FOR NOW
    /*
    private IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(1.75f);
        //removing any RB physics effects from previous interactions
        objectRB.velocity = new Vector3(0, 0, 0);
        objectRB.angularVelocity = new Vector3(0, 0, 0);

        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
    */

    //set object inactive, after a delay
    private IEnumerator DestroyObjectAndSetTrayObjectActive()
    {
        yield return new WaitForSeconds(1f);
        gameManagerScript.objectsInScene--;
        gameObject.SetActive(false);
        correctParticle.SetActive(false);

        //activate relevant sorted object
        gameManagerScript.ActivateSortedObject();
    }

    //turn on particle effect
    private IEnumerator TurnOnWrongParticle()
    {
        yield return new WaitForSeconds(1.15f);
        wrongParticle.SetActive(true);
        
    }
    //turn off particle effect
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
            if (other.CompareTag(gameObject.tag))
            {
                //audioManager sound effects:
                audioManagerScript.WhenToyPutInCorrectTray();

                StartCoroutine(DestroyObjectAndSetTrayObjectActive());
                //play 'correct' sound effect
                gameManagerScript.gameAudio.PlayOneShot(gameManagerScript.correctSound);
                correctParticle.SetActive(true);
            }
            //if the object is a different colour, bounce object vertically
            else if (!other.CompareTag(colour))
            {
                //wrongParticle.SetActive(true);
                StartCoroutine(TurnOnWrongParticle());
                StartCoroutine(TurnOffWrongParticle());

                //play 'wrong' bleep sound effect
                gameManagerScript.gameAudio.PlayOneShot(gameManagerScript.wrongSound);
                
                //bespoke sound effects from audioManager
                audioManagerScript.AdviceForWrongTray_MaleVoice(GetColourNumber(), GetWrongTrayNumber(other.tag));

                objectRB.AddForce(new Vector3(0, 1.2f, 0.10f) * 18f, ForceMode.Impulse);

                //transporting to a random position - perhaps fix below method if possible
                StartCoroutine(ReturnToRandomPosition());

                //KEEP FOR NOW
                //return to original position after a moment. NB - object pooling has made this more difficult
                //StartCoroutine(ReturnToOriginalPosition());
            }
        }
    }

    private int GetColourNumber()
    {
        return determineColourOfObject(gameManagerScript.determineWhatObjectIsPickedUp());
    }

    private int determineColourOfObject(int objectPickedUp)
    {
        if (objectPickedUp == 1)
        {
            return colourSelectorScript.currentLeft;
        }
        else if (objectPickedUp == 2)
        {
            return colourSelectorScript.currentMiddle;
        }
        else
            return colourSelectorScript.currentRight;
    }

    /*
    private int determineWhatObjectIsPickedUp()
    {
        if (gameManagerScript.TrayPositionOfPickedUpObject == "left")
        {
            return 1;
        }
        else if (gameManagerScript.TrayPositionOfPickedUpObject == "middle")
        {
            return 2;
        }
        else if (gameManagerScript.TrayPositionOfPickedUpObject == "right")
        {
            return 3;
        }
        else return 0;
    }
    */

    private int GetWrongTrayNumber(string trayTag)
    {
        if (trayTag == "left")
        {
            return colourSelectorScript.currentLeft;
        }
        else if (trayTag == "middle")
        {
            return colourSelectorScript.currentMiddle;
        }
        else
        {
            return colourSelectorScript.currentRight;
        }
    }

}