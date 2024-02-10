using UnityEngine;
using UnityEngine.InputSystem;

public class bl_ControllerExample : MonoBehaviour {

    /// <summary>
    /// Step #1
    /// We need a simple reference of joystick in the script
    /// that we need add it.
    /// </summary>
	[SerializeField]private bl_Joystick Joystick;//Joystick reference for assign in inspector

    [SerializeField]private float movementSpeed = 2;

    void Update()
    {
        //Step #2
        //Change Input.GetAxis (or the input that you using) to Joystick.Vertical or Joystick.Horizontal
        float v = Joystick.Vertical; //get the vertical value of joystick
        float h = Joystick.Horizontal;//get the horizontal value of joystick

        //in case you using keys instead of axis (due keys are bool and not float) you can do this:
        //bool isKeyPressed = (Joystick.Horizontal > 0) ? true : false;

        //ready!, you not need more.
        Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * movementSpeed;
        transform.Translate(translate);


        /*
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
        */

    }
}