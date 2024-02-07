using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PS4Controller : MonoBehaviour
{
    GameObject cube = null;

    float movementSpeed = 5f;
    float rotationSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        //printing to log to check controller/s are connected
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }

        //find cube object
        cube = GameObject.Find("PS Controller Cube");
    }

    // Update is called once per frame
    void Update()
    {
        //applying basic movement controls to the left joystick of PS controller
        //forward, backward, side-to-side
        //NB. currently using a bool (isPressed) rather than getting joystick axis
        if (Gamepad.all.Count > 0)
        {
            //left movement
            if (Gamepad.all[0].leftStick.left.isPressed)
            {
                cube.transform.position += Vector3.left * Time.deltaTime * movementSpeed;
            }
            //right movement
            if (Gamepad.all[0].leftStick.right.isPressed)
            {
                cube.transform.position += Vector3.right * Time.deltaTime * movementSpeed;
            }
            //forward movemment
            if (Gamepad.all[0].leftStick.up.isPressed)
            {
                cube.transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
            }
            //backward movement
            if (Gamepad.all[0].leftStick.down.isPressed)
            {
                cube.transform.position += Vector3.back * Time.deltaTime * movementSpeed;
            }


            //right and left rotation
            if (Gamepad.all[0].rightStick.right.isPressed)
            {
                cube.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
            }

            if (Gamepad.all[0].rightStick.left.isPressed)
            {
                cube.transform.Rotate(Vector3.down, Time.deltaTime * rotationSpeed);
            }


        }
    }
}
