using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSorting_PerimeterManager : MonoBehaviour
{

    private ShapeSorting_GameManager gameManagerScript;
    public int shapeToReturn;


    // Start is called before the first frame update
    void Start()
    {

        //getting GameManager
        gameManagerScript = GameObject.Find("GameManager").GetComponent<ShapeSorting_GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        gameManagerScript.shapeToReturn = IdentifyObjectNumber(other.tag);
        gameManagerScript.shapeReturning = true;

    }

    
    private int IdentifyObjectNumber(string Name)
    {
        if (Name == "Pentagon")
        {
            return 0;
        }
        else if (Name == "Arch")
        {
            return 1;
        }
        else if (Name == "Circle")
        {
            return 2;
        }
        else if (Name == "Rectangle")
        {
            return 3;
        }
        else if (Name == "Star")
        {
            return 4;
        }
        else if (Name == "Square")
        {
            return 5;
        }
        else
            return 0;

    }
    
}
