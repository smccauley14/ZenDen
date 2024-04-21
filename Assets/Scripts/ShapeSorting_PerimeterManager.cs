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

    private void OnTriggerEnter(Collider other)
    {
        gameManagerScript.shapeToReturn = IdentifyObjectNumber(other.tag);
        gameManagerScript.shapeOutOfBounds = true;
    }

    private int IdentifyObjectNumber(string NameOfShape)
    {
        if (NameOfShape == "Pentagon")
        {
            return 0;
        }
        else if (NameOfShape == "Arch")
        {
            return 1;
        }
        else if (NameOfShape == "Circle")
        {
            return 2;
        }
        else if (NameOfShape == "Rectangle")
        {
            return 3;
        }
        else if (NameOfShape == "Star")
        {
            return 4;
        }
        else if (NameOfShape == "Square")
        {
            return 5;
        }
        else
            return 0;
    }
}
