using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PS4Controller : MonoBehaviour
{
    GameObject cube = null;

    // Start is called before the first frame update
    void Start()
    {
        //printing to log to check controller/s are connected
        for (int i =0; i< Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }

        //find cube object
        cube = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        //applying basic movement controls to the left joystick of PS controller
        //forward, backward, side-to-side
        //NB. currently using a bool (isPressed) rather than getting joystick axis
        if (Gamepad.all.Count > 0)
        {
            if (Gamepad.all[0].leftStick.left.isPressed)
            {
                cube.transform.position += Vector3.left * Time.deltaTime * 5f;
            }

            if (Gamepad.all[0].leftStick.right.isPressed)
            {
                cube.transform.position += Vector3.right * Time.deltaTime * 5f;
            }

            if (Gamepad.all[0].leftStick.up.isPressed)
            {
                cube.transform.position += Vector3.forward * Time.deltaTime * 5f;
            }

            if (Gamepad.all[0].leftStick.down.isPressed)
            {
                cube.transform.position += Vector3.back * Time.deltaTime * 5f;
            }

        }
    }
}