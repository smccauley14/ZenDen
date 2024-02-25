using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //variables
    [HideInInspector] public bool isDragging;
    //perhaps this could be more efficient if it was a static variable within 'DragDropToForeground' script
    //but I struggled to get it to work that way

    //for debugging - canvas object appears
    //public GameObject DebugDragText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DebugDraggingMessage();
    }

    //for DEBUGGING shows message on screen when dragging is occurring
    /*
    private void DebugDraggingMessage()
    {

        if (!isDragging)
        {
            DebugDragText.SetActive(false);

        }
        else if (isDragging)
        {
            DebugDragText.SetActive(true);
        }
        
    }
    */
}
