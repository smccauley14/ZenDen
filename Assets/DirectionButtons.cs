using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class DirectionButtons : MonoBehaviour
{
    private float movementSpeed = 5f;
    private float rotationSpeed = 60f;
    public Button forwardButton;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        //FORWARD BUTTON
        if (Gamepad.all[0].dpad.up.isPressed)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
        }


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
        //right and left rotation
        if (Gamepad.all[0].rightStick.right.isPressed)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }

        if (Gamepad.all[0].rightStick.left.isPressed)
        {
            transform.Rotate(Vector3.down, Time.deltaTime * rotationSpeed);
        }

    }


    private void forwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
    
}
