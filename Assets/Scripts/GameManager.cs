using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //variables
    [HideInInspector] public bool isDragging;
    public GameObject dragText;//for debugging - canvas object appears

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUGGING - shows on screen is a method is currently being dragged
        if (!isDragging)
        {
            dragText.SetActive(false);

        }
        else if (isDragging)
        {
            dragText.SetActive(true);

        }
    }
}
