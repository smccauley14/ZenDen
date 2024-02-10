using UnityEngine;
using UnityEngine.InputSystem;

public class RightScreenController : MonoBehaviour
{

    /// <summary>
    /// Step #1
    /// We need a simple reference of joystick in the script
    /// that we need add it.
    /// </summary>
	[SerializeField] private bl_Joystick Joystick;//Joystick reference for assign in inspector

    [SerializeField] private float rotationSpeed = 60;

    void Update()
    {
        //Step #2
        //Change Input.GetAxis (or the input that you using) to Joystick.Vertical or Joystick.Horizontal
        //float v = Joystick.Vertical; //get the vertical value of joystick
        float h = Joystick.Horizontal;//get the horizontal value of joystick

        //in case you using keys instead of axis (due keys are bool and not float) you can do this:
        //bool isKeyPressed = (Joystick.Horizontal > 0) ? true : false;

        //ready!, you not need more.
        Vector3 translate = (new Vector3(0, h, 0) * Time.deltaTime) * rotationSpeed;
        transform.Rotate(translate);

        /*
        //right and left rotation
        if (Gamepad.all[0].rightStick.right.isPressed)
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
        }

        if (Gamepad.all[0].rightStick.left.isPressed)
        {
            transform.Rotate(Vector3.down, Time.deltaTime * rotationSpeed);
        }
        */

    }
}
