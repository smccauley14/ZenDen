using UnityEngine;
using UnityEngine.InputSystem;

public class PS4Controller : MonoBehaviour
{
    //variables
    float movementSpeed = 5f;
    float rotationSpeed = 80f;

    // Start is called before the first frame update
    void Start()
    {
        //printing to log to check controller/s are connected
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //calling method for gamepad controls
        GamepadControls();

    }


    private void GamepadControls()
    {
        //applying basic movement controls to the left joystick of PS controller
        //forward, backward, side-to-side
        //NB. currently using a bool (isPressed) rather than getting joystick axis

        if (Gamepad.all.Count > 0)//if at least one gamepad is connect
        {
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


            //right stick rotation controls
            if (Gamepad.all[0].rightStick.right.isPressed)
            {
                transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
            }

            //left rotation
            if (Gamepad.all[0].rightStick.left.isPressed)
            {
                transform.Rotate(Vector3.down, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
