using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DirectionButtons : MonoBehaviour
{
    public float movementSpeed = 0.1f;
    public Button forwardButton;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        forwardButton.onClick.AddListener(forwardMovement);

       //forwardButton.OnPointerDown();



    }

    /*
    private bool forwardButtonPressed()
    {

        if (forwardButton.onClick)
        {
            return true;
        }

        return false;
    }
    */

    private void forwardMovement()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);
    }
    
}
