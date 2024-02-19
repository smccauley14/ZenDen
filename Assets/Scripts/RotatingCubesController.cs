using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RotatingCubesController : MonoBehaviour
{

    /*
    [SerializeField] InputAction press;
    private Vector2 touchStartPos;
    //private bool isTouching = false;
    [SerializeField] Camera playerCamera;
    private Vector3 currentScreenPosition;
    */

    
    private float _sensitivity;
    private Vector3 _mouseReference;
    private Vector3 _mouseOffset;
    private Vector3 _rotation;
    private bool _isRotating;

    void Start()
    {
        _sensitivity = 0.2f;
        _rotation = Vector3.zero;
    }

    void Update()
    {
        if (_isRotating)
        {
            // offset
            _mouseOffset = (Input.mousePosition - _mouseReference);

            // apply rotation
            _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
            // apply rotation
            //_rotation.x = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;

            // rotate
            transform.Rotate(_rotation);

            // store mouse
            _mouseReference = Input.mousePosition;
        }
    }

    void OnMouseDown()
    {
        // rotating flag
        _isRotating = true;

        // store mouse
        _mouseReference = Input.mousePosition;
    }

    void OnMouseUp()
    {
        // rotating flag
        _isRotating = false;
    }

    void HorizOrVert()
    {

    }


    /*
private void Awake()
    {
        //enabling interaction events
        press.Enable();

        press.performed += _ =>
        {
            
        };

    }
    */
    
}