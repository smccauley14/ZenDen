using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragDropItems : MonoBehaviour
{

    [SerializeField] InputAction press;
    [SerializeField] InputAction screenPosition;

    private Vector3 currentScreenPosition;

    [SerializeField] Camera playerCamera;
    private bool isDragging;
    private Vector3 worldPosition
    {
        get
        {
            float z = playerCamera.WorldToScreenPoint(transform.position).z;
            return playerCamera.ScreenToWorldPoint(currentScreenPosition + new Vector3 (0,0,z));
        }
    }

    private bool isClickedOn
    {
        get
        {
            Ray  ray = playerCamera.ScreenPointToRay(currentScreenPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit ))
            {
                return hit.transform == transform;
            }
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        screenPosition.Enable();
        press.Enable();
        screenPosition.performed += context => { currentScreenPosition = context.ReadValue<Vector2>(); };
        press.performed += _ => { if (isClickedOn) StartCoroutine(Drag()); };
        press.canceled += _ => { isDragging = false; };

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private IEnumerator Drag()
    {
        isDragging = true;
        Vector3 offset = transform.position - worldPosition;
        //grab
        GetComponent<Rigidbody>().useGravity = false;
        while(isDragging)
        {
            //dragging
            transform.position = worldPosition + offset;
            yield return null;
        }
        //drop
        GetComponent<Rigidbody>().useGravity = true;
    }

}
