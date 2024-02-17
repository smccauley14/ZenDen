using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropToForeground : MonoBehaviour
{
    //followed this tutorial: https://www.youtube.com/watch?v=zo1dkYfIJVg

    //variables
    [SerializeField] InputAction press;
    [SerializeField] InputAction screenPosition;
    [SerializeField] Camera playerCamera;
    private Vector3 currentScreenPosition;
    private float targetZ = 0.5f;
    private GameManager gameManager;
    //public bool isDragging = false; - not needed?

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
            if (Physics.Raycast(ray, out hit))
            {
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
    }

    private void Awake()
    {
        //enabling interaction events
        screenPosition.Enable();
        press.Enable();

        //to allow you to assess where on the screen has been clicked (I think)
        screenPosition.performed += context => { currentScreenPosition = context.ReadValue<Vector2>(); };

        //declaring what should happen press interaction starts
        press.performed += _ => { if (isClickedOn) StartCoroutine(Drag()); };

        //declaring what should happen press interaction ends
        press.canceled += _ =>
        {
            //isDragging = false; - npt needed; just refer to gameManager 'isDragging instead'
            gameManager.isDragging = false;
        };


    }

    // Update is called once per frame
    void Update()
    {


    }


    private IEnumerator Drag()
    {
        //making both 'isDragging' scripts true when object interaction is taking place
        //isDragging = true;
        gameManager.isDragging = true;

        //finding the necessary offset
        Vector3 offset = transform.position - worldPosition;

        //turn off RB
        GetComponent<Rigidbody>().useGravity = false;

        //pulling object into foreground
        transform.position = new Vector3(0, 0, targetZ);

        //drag along X axis
        while (gameManager.isDragging)
        {
            //dragging
            transform.position = worldPosition + offset;
            yield return null;
        }
        //droping object - turn RB back on
        GetComponent<Rigidbody>().useGravity = true;
    }

}