using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

//attach script to player object

public class OnScreenController : MonoBehaviour
{

    //variables
    private float movementSpeed = 3f;
    private float rotationSpeed = 80f;
    [SerializeField] Button forwardButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject cameraObject;
    private Transform cameraTransform;

    private float cameraMaxAngle = 10f;


    // Start is called before the first frame update
    void Start()
    {
        //get tranform of camera
        cameraTransform = cameraObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        //calling controller methods
        JoystickControls();
        DpadControls();

    }


    private void DpadControls()
    {
        //FORWARD BUTTON
        if (Gamepad.all[0].dpad.up.isPressed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }

        //BACKWARD BUTTON
        if (Gamepad.all[0].dpad.down.isPressed)
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }

        //add right and left if this is something we want to implement
    }

    private void JoystickControls()
    {
        //LEFT STICK CONTROLLER:

        //left movement
        if (Gamepad.all[0].leftStick.left.isPressed)
        {
            transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
        }
        //right movement
        if (Gamepad.all[0].leftStick.right.isPressed)
        {
            transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
        }
        //forward movemment
        if (Gamepad.all[0].leftStick.up.isPressed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }
        //backward movement
        if (Gamepad.all[0].leftStick.down.isPressed)
        {
            transform.Translate(Vector3.back * Time.deltaTime * movementSpeed);
        }

        //RIGHT JOYSTICK
        //right rotation
        if (Gamepad.all[0].rightStick.right.isPressed)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }

        //left rotation
        if (Gamepad.all[0].rightStick.left.isPressed)
        {
            transform.Rotate(Vector3.down, Time.deltaTime * rotationSpeed);
        }
        
        //vertical rotation up (max angle not working)
        if (Gamepad.all[0].rightStick.up.isPressed && cameraTransform.rotation.x > -cameraMaxAngle)
        {
            cameraTransform.Rotate(Vector3.left, Time.deltaTime * rotationSpeed);
        }

        //vertical rotation down (max angle not working)
        if (Gamepad.all[0].rightStick.down.isPressed && cameraTransform.rotation.x < cameraMaxAngle)
        {
            cameraTransform.transform.Rotate(Vector3.right, Time.deltaTime * rotationSpeed);
        }
        


    }

}
