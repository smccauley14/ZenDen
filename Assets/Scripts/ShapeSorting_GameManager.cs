using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSorting_GameManager : MonoBehaviour
{

    private Vector3[] receiverLocations = new Vector3[6];
    private Vector3[] shapeLocations = new Vector3[6];
    [SerializeField] private GameObject[] shapeHoles = new GameObject[6];
    [SerializeField] private GameObject[] shapeObjects = new GameObject[6];

    [HideInInspector] public int shapesInScene = 6;
    private int waveCount;


    // Start is called before the first frame update
    void Start()
    {


        GetStartingPositionsOfHoles();

        GetStartingPositionsOfShapes();


    }

    // Update is called once per frame
    void Update()
    {

        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Randomly swap the positions of the objects
            RandomizeHolePositions();
        }

        // Check if the Escape key is pressed
        if (Input.GetKeyDown(KeyCode.W))
        {
            // Randomly swap the positions of the objects
            RandomizeShapePositions();
        }

        if(shapesInScene <1)
        {
            RespawnShapesInNewPositions();
            shapesInScene = 6;
        }

    }

    void RespawnShapesInNewPositions()
    {
        RandomizeHolePositions();
        RandomizeShapePositions();

        for (int i = 0; i < 6; i++)
        {
            shapeObjects[i].SetActive(true);
        }
    }

    void GetStartingPositionsOfHoles()
    {
        //getting initial positions of each hole
        for (int i = 0; i < 6; i++)
        {
            receiverLocations[i] = shapeHoles[i].transform.position;
        }
    }

    void GetStartingPositionsOfShapes()
    {
        //getting initial positions of each hole
        for (int i = 0; i < 6; i++)
        {
            shapeLocations[i] = shapeObjects[i].transform.position;
        }

    }

    void RandomizeHolePositions()
    {
        // Shuffle the array of initial positions
        ShuffleArray(receiverLocations);

        // Assign the shuffled positions to the objects
        for (int i = 0; i < shapeHoles.Length; i++)
        {
            shapeHoles[i].transform.position = receiverLocations[i];
        }
    }

    void RandomizeShapePositions()
    {
        // Shuffle the array of initial positions
        ShuffleArray(shapeLocations);

        // Assign the shuffled positions to the objects
        for (int i = 0; i < shapeObjects.Length; i++)
        {
            shapeObjects[i].transform.position = shapeLocations[i];
        }
    }

    void ShuffleArray(Vector3[] array)
    {
        //shuffle algorithm
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Vector3 temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

}
