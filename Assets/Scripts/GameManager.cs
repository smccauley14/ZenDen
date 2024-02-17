using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    //variables
    [HideInInspector] public bool isDragging;

    //for debugging - canvas object appears
    public GameObject debugDragText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //DebugDraggingMessage();
    }

    //for DEBUGGING shows message on screen when dragging is occurring
    private void DebugDraggingMessage()
    {

        if (!isDragging)
        {
            debugDragText.SetActive(false);

        }
        else if (isDragging)
        {
            debugDragText.SetActive(true);
        }
        
    }
}
